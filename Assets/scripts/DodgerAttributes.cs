public class DodgerAttributes
{
    private int currentHealth;
    private int maxHealth;
    private int currentScore;

    // Constructor
    public DodgerAttributes(int startHealth, int startMaxHealth, int startScore)
    {
        currentHealth = startHealth;
        maxHealth = startMaxHealth;
        currentScore = startScore;
    }

    // Getters
    public int GetCurrentHealth()
    {
        return currentHealth;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public int GetCurrentScore()
    {
        return currentScore;
    }

    // Setters
    public void SetCurrentHealth(int newHealth)
    {
        currentHealth = newHealth;
    }

    public void SetCurrentScore(int newScore)
    {
        currentScore = newScore;
    }
}
