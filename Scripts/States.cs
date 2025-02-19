public static class States
{
    public enum GameState
    {
        Playing,
        GameOver
    }

    public static GameState currentState = GameState.Playing;
}