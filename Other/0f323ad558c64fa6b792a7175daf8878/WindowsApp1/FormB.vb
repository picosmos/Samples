Imports WeifenLuo.WinFormsUI.Docking

Public Class FormB
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        RaiseEvent MyTextChanged(Me, TextBox1.Text)
    End Sub

    Public Event MyTextChanged As EventHandler(Of String)

End Class