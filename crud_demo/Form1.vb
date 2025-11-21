
Imports MySql.Data.MySqlClient
Public Class Form1
    Dim conn As MySqlConnection
    Dim COMMAND As MySqlCommand
    Private Sub ButtonConnect_Click(sender As Object, e As EventArgs) Handles ButtonConnect.Click
        conn = New MySqlConnection
        conn.ConnectionString = "server=localhost; userid=root; password=root; database= crud_demo_db"

        Try
            conn.Open()
            MessageBox.Show("Connected")
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            conn.Close()
        End Try
    End Sub

    Private Sub ButtonInsert_Click(sender As Object, e As EventArgs) Handles ButtonInsert.Click

        Dim query As String = "INSERT INTO students_tbl (name, age, email) VALUES (@name, @age, @email)"

        Try
            Using conn As New MySqlConnection("server=localhost; userid=root; password=root; database= crud_demo_db")
                conn.Open()
            End Using
            Using cmd As New MySqlCommand(query, conn)
                    cmd.Parameters.AddWithValue("@name", TextBoxName.Text)
                    cmd.Parameters.AddWithValue("@age", TextBoxAge.Text)
                cmd.Parameters.AddWithValue("@email", TextBoxEmail.Text)
                cmd.ExecuteNonQuery()
                MessageBox.Show("Record insert succesful", "Record Info", MessageBoxButtons.OK, MessageBoxIcon.Information)

            End Using
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub ButtonRead_Click(sender As Object, e As EventArgs) Handles ButtonRead.Click
        Dim query As String = "SELECT * FROM crud_demo_db.students_tbl;"

        Try
            Using conn As New MySqlConnection("server=localhost; userid=root; password=root; database= crud_demo_db")
                Dim adapter As New MySqlDataAdapter(query, conn) 'Get from database
                Dim table As New DataTable() 'table object
                adapter.Fill(table) 'from adapter to table object
                DataGridView1.DataSource = table 'Display to Datagridview

            End Using
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Sub ButtonEdit_Click(sender As Object, e As EventArgs) Handles ButtonEdit.Click

        If DataGridView1.SelectedRows.Count = 0 Then
            MessageBox.Show("Select row to edit.", "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        TextBoxName.Text = DataGridView1.SelectedRows(0).Cells("name").Value.ToString()
        TextBoxAge.Text = DataGridView1.SelectedRows(0).Cells("age").Value.ToString()
        TextBoxEmail.Text = DataGridView1.SelectedRows(0).Cells("email").Value.ToString()

    End Sub
    Private Sub ButtonUpdate_Click(sender As Object, e As EventArgs) Handles ButtonUpdate.Click

        If DataGridView1.SelectedRows.Count = 0 Then
            MessageBox.Show("Please select a record to update.", "Reminder", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        Dim id As Integer = DataGridView1.SelectedRows(0).Cells("id").Value

        Dim query As String = "UPDATE students_tbl SET name=@name, age=@age, email=@email WHERE id=@id"

        Try
            Using conn As New MySqlConnection("server=localhost; userid=root; password=root; database= crud_demo_db")
                conn.Open()
            End Using

            Using cmd As New MySqlCommand(query, conn)
                cmd.Parameters.AddWithValue("@name", TextBoxName.Text)
                cmd.Parameters.AddWithValue("@age", TextBoxAge.Text)
                cmd.Parameters.AddWithValue("@email", TextBoxEmail.Text)
                cmd.Parameters.AddWithValue("@id", id)

                cmd.ExecuteNonQuery()
                MessageBox.Show("Record updated successfully!", "Update Info", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Using

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub


End Class
