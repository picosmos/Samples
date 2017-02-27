Class MainWindow



    Private Sub UIElement_OnMouseDown(sender As Object, e As MouseButtonEventArgs)
        Dim pos = e.GetPosition(DirectCast(sender, IInputElement))
        ObjectMouseDown(pos.X, pos.Y)
    End Sub

    Private Sub UIElement_OnMouseMove(sender As Object, e As MouseEventArgs)
        if (e.LeftButton = MouseButtonState.Pressed)
            Dim pos = e.GetPosition(DirectCast(sender, IInputElement))
            ObjectMove(Me, pos.X, pos.Y)
        End If
    End Sub

End Class


Module Module_Move
    Dim X1, Y1 As Integer

    Sub ObjectMouseDown(ByVal X As Integer, ByVal Y As Integer)
        X1 = X
        Y1 = Y
    End Sub

    Sub ObjectMove(ByVal Obj As Object, ByVal X2 As Integer, ByVal Y2 As Integer)
        Obj.Left = Obj.Left + (X2 - X1)
        Obj.Top = Obj.Top + (Y2 - Y1)
    End Sub
End Module