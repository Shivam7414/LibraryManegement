Public Class AdminForm
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If PassTb.Text = "" Then
            MsgBox("Enter the Admin Password")
        ElseIf PassTb.Text = "Admin" Then
            Dim obj = New Librarians
            obj.Show()
            Me.Hide()
        Else
            MsgBox("Wrong Password")
        End If

    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click
        Dim obj = New Login
        obj.Show()
        Me.Hide()
    End Sub
End Class