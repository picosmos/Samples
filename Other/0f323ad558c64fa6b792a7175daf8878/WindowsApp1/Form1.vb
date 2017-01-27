Imports WeifenLuo.WinFormsUI.Docking

Public Class Form1


    Private a AS FormA
    Private b As FormB

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        a = New FormA()
        a.Show(Me.Panel1, DockState.Document)


        b = New FormB()
        b.Show(Me.Panel1, DockState.Document)
        'AddHandler b.MyTextChanged, b_MyTextChanged

    End Sub

    Private Sub MyTextChanged(sender As Object, e As String)
        a.ShowNewText(e)
    End Sub

End Class
