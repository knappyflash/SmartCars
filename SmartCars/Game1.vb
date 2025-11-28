Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics
Imports Microsoft.Xna.Framework.Input

Public Class Game1
    Inherits Game

    Private graphics As GraphicsDeviceManager
    Private spriteBatch As SpriteBatch

    ' Rectangle state
    Private rectTexture As Texture2D
    Private rectPosition As Vector2
    Private rectSize As New Point(100, 50) ' width=100, height=50
    Private rectSpeed As Single = 5.0F
    Private rotation As Single = MathHelper.ToRadians(45)
    Private rectRotation As Single = 0.0F
    Private rectRotationSpeed As Single = 0.02F ' radians per frame



    Public Sub New()
        graphics = New GraphicsDeviceManager(Me)
        Content.RootDirectory = "Content"
        IsMouseVisible = True
    End Sub

    Protected Overrides Sub Initialize()
        graphics.PreferredBackBufferWidth = 800
        graphics.PreferredBackBufferHeight = 600
        'graphics.IsFullScreen = True
        graphics.ApplyChanges()

        ' Start rectangle in the middle of the screen
        rectPosition = New Vector2(350, 275)

        MyBase.Initialize()
    End Sub

    Protected Overrides Sub LoadContent()
        spriteBatch = New SpriteBatch(GraphicsDevice)

        ' Create a 1x1 white texture we can scale
        rectTexture = New Texture2D(GraphicsDevice, 1, 1)
        rectTexture.SetData(New Color() {Color.White})
    End Sub

    Protected Overrides Sub Update(gameTime As GameTime)
        Dim state = Keyboard.GetState()

        ' Escape closes the game
        If state.IsKeyDown(Keys.Escape) Then
            Me.Exit()
        End If

        ' Movement
        If state.IsKeyDown(Keys.A) Then rectPosition.X -= rectSpeed
        If state.IsKeyDown(Keys.D) Then rectPosition.X += rectSpeed
        If state.IsKeyDown(Keys.W) Then rectPosition.Y -= rectSpeed
        If state.IsKeyDown(Keys.S) Then rectPosition.Y += rectSpeed

        ' Rotate with keys
        If state.IsKeyDown(Keys.Q) Then rectRotation -= rectRotationSpeed
        If state.IsKeyDown(Keys.E) Then rectRotation += rectRotationSpeed

        MyBase.Update(gameTime)
    End Sub


    Protected Overrides Sub Draw(gameTime As GameTime)
        GraphicsDevice.Clear(Color.CornflowerBlue)

        Dim origin As New Vector2(rectSize.X / 2, rectSize.Y / 2)

        spriteBatch.Begin()
        spriteBatch.Draw(rectTexture,
                         rectPosition,
                         Nothing,
                         Color.Red,
                         rectRotation,                 ' use dynamic rotation
                         origin,
                         New Vector2(rectSize.X, rectSize.Y),
                         SpriteEffects.None,
                         0.0F)
        spriteBatch.End()

        MyBase.Draw(gameTime)
    End Sub

End Class
