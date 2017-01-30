Imports WeifenLuo.WinFormsUI.Docking

Public Class FormB
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        RaiseEvent MyTextChanged(Me, New MyEventArgs() With { .MyString = TextBox1.Text })
    End Sub

    Public Event MyTextChanged As EventHandler(Of MyEventArgs)

End Class