Imports System.Data.SQLite
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class Membership
    Private connectionString As String = "DataSource=C:\Users\Steve\Desktop\Visual Basic Programming Projects\LibManager.db"
    Private Function GetDataTable() As DataTable
        Dim dataTable As New DataTable()

        Using connection As New SQLiteConnection(connectionString)
            connection.Open()

            Dim command As New SQLiteCommand("SELECT * FROM Membership", connection)
            Dim reader As SQLiteDataReader = command.ExecuteReader()

            dataTable.Load(reader)
        End Using

        Return dataTable
    End Function
    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim Name As String = TextBox1.Text
        Dim Membership_ID As String = TextBox2.Text
        Dim Contact_Info As String = TextBox3.Text
        Dim Membership_Date As String = TextBox4.Text



        If Name.Trim() = "" Or Membership_ID.Trim() = "" Or Contact_Info.Trim() = "" Or Membership_Date.Trim() = "" Then
            MessageBox.Show("Please enter all the fields.")
            Exit Sub
        End If

        Using connection As New SQLiteConnection(connectionString)
            connection.Open()
            Dim command As New SQLiteCommand("INSERT INTO Membership (Name,Membership_ID,Contact_Info,Membership_Date) VALUES (@Name,@Membership_ID,@Contact_Info,@Membership_Date)", connection)
            command.Parameters.AddWithValue("@Name", Name)
            command.Parameters.AddWithValue("@Membership_ID", Membership_ID)
            command.Parameters.AddWithValue("@Contact_Info", Contact_Info)
            command.Parameters.AddWithValue("@Membership_Date", Membership_Date)


            Try
                ' Execute the SQL command
                command.ExecuteNonQuery()
                MessageBox.Show(" " & TextBox1.Text & " is now a Member")
                Dim dataTable = GetDataTable() ' Replace with your data retrieval method
                DataGridView1.DataSource = dataTable
            Catch ex As SQLiteException
                ' Handle the specific SQLite exception
                If ex.ErrorCode = SQLiteErrorCode.Constraint Then
                    ' Check if the exception is due to a primary key constraint violation
                    MessageBox.Show("ID Assigned to a member Already " & ex.Message)
                Else
                    ' Handle other SQLite exceptions
                    MessageBox.Show("SQLite error: " & ex.Message)
                End If
            Catch ex As Exception
                ' Handle other general exceptions
                MessageBox.Show("An error occurred: " & ex.Message)
            End Try


            TextBox1.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            TextBox4.Text = ""
            txtSearch.Text = ""
        End Using
    End Sub
    Private Sub Membership_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim dataTable = GetDataTable()
        DataGridView1.DataSource = dataTable
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim Name As String = TextBox1.Text
        Dim Membership_ID As String = TextBox2.Text
        Dim Contact_Info As String = TextBox3.Text
        Dim Membership_Date As String = TextBox4.Text

        Using connection As New SQLiteConnection(connectionString)
            connection.Open()

            Dim command As New SQLiteCommand("UPDATE Membership SET Name = @Name, Membership_ID = @Membership_ID,Contact_Info = @Contact_Info,Membership_Date= @Membership_Date WHERE Membership_ID = @Membership_ID", connection)
            command.Parameters.AddWithValue("@Name", Name)
            command.Parameters.AddWithValue("@Membership_ID", Membership_ID)
            command.Parameters.AddWithValue("@Contact_Info", Contact_Info)
            command.Parameters.AddWithValue("@Membership_Date", Membership_Date)
            Dim rowsAffected As Integer = command.ExecuteNonQuery()

            If rowsAffected > 0 Then
                If MessageBox.Show(" " & TextBox1.Text & " Updated successfully!", "Confirmation", MessageBoxButtons.OK) = DialogResult.OK Then
                    ' Refresh DataGridView here
                    Dim dataTable = GetDataTable() ' Replace with your data retrieval method
                    DataGridView1.DataSource = dataTable
                End If
                TextBox1.Text = ""
                TextBox2.Text = ""
                TextBox3.Text = ""
                TextBox4.Text = ""
                txtsearch.Text = ""
            Else
                MessageBox.Show("No data updated.")
            End If
        End Using
    End Sub
    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        If e.RowIndex >= 0 Then

            ' Get the selected row
            Dim selectedRow As DataGridViewRow = DataGridView1.Rows(e.RowIndex)


            ' Access data from the selected row
            TextBox1.Text = selectedRow.Cells(0).Value.ToString()
            TextBox2.Text = selectedRow.Cells(1).Value.ToString()
            TextBox3.Text = selectedRow.Cells(2).Value.ToString()
            TextBox4.Text = selectedRow.Cells(3).Value.ToString()



        End If
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim Name As String = TextBox1.Text
        Dim Membership_ID As String = TextBox2.Text
        Dim Contact_Info As String = TextBox3.Text
        Dim Membership_Date As String = TextBox4.Text

        If Membership_ID.Trim() = "" Then
            MessageBox.Show("Please enter Membership_ID to delete")
            Exit Sub
        End If

        Using connection As New SQLiteConnection(connectionString)
            connection.Open()
            Dim command As New SQLiteCommand("Delete from Membership where Name=@Name", connection)
            command.Parameters.AddWithValue("@Name", Name)
            command.Parameters.AddWithValue("@Membership_ID", Membership_ID)
            command.Parameters.AddWithValue("@Contact_Info", Contact_Info)
            command.Parameters.AddWithValue("@Membership_Date", Membership_Date)
            Dim rowsAffected As Integer = command.ExecuteNonQuery()

            If MessageBox.Show("Data deleted successfully!", "Confirmation", MessageBoxButtons.OK) = DialogResult.OK Then
                ' Refresh DataGridView here
                Dim dataTable = GetDataTable() ' Replace with your data retrieval method
                DataGridView1.DataSource = dataTable
            End If
            TextBox1.Clear()
            TextBox2.Clear()
            TextBox3.Clear()
            TextBox4.Clear()
            txtSearch.Clear()
        End Using
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim searchText As String = txtSearch.Text.ToLower()

        If Not String.IsNullOrEmpty(searchText) Then
            Dim filteredData = GetFilteredData(searchText)
            DataGridView1.DataSource = filteredData
        Else
            MessageBox.Show("Please enter a search Field.")
            Dim dataTable = GetDataTable() ' Replace with your data retrieval method
            DataGridView1.DataSource = dataTable
        End If
    End Sub
    Private Function GetFilteredData(searchText As String) As DataTable
        Dim filteredData As New DataTable()

        Using connection As New SQLiteConnection(connectionString)
            connection.Open()

            Dim sql = String.Format("SELECT * FROM Membership WHERE LOWER(Name) LIKE '%{0}%' OR LOWER(Membership_ID) LIKE '%{0}%'", searchText)
            Dim command As New SQLiteCommand(sql, connection)
            Dim reader As SQLiteDataReader = command.ExecuteReader()

            filteredData.Load(reader)
        End Using

        Return filteredData
    End Function

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles IDMaker.Click
        Dim r As Random = New Random
        Dim num As Integer
        num = (r.Next(1, 999999999))
        Dim IDMaker As String = Strings.Right("000000000" & num.ToString(), 9)
        TextBox2.Text = IDMaker

        Dim SQLiteCon As New SQLiteConnection(connectionString)

        Try
            SQLiteCon.Open()

        Catch ex As Exception
            SQLiteCon.Dispose()
            SQLiteCon = Nothing
            MessageBox.Show(ex.Message)
            Exit Sub
        End Try
        Dim TableDB As New DataTable
        Try
            Dim sql = String.Format("select membership_id from  membership where membership_id='" & TextBox2.Text & "'", TableDB, SQLiteCon)
        Catch ex As Exception

            MessageBox.Show(ex.Message)


        End Try
        If TableDB.Rows.Count > 0 Then
            Button5_Click(sender, e)
        End If

    End Sub
End Class
