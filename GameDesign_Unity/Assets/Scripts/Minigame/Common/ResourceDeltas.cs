[System.Serializable]
public class ResourceDeltas
{
    public int stressDelta;
    public int moneyDelta;
    public int sympathyDelta;

    public ResourceDeltas(int stressDelta, int moneyDelta, int sympathyDelta)
    {
        this.stressDelta = stressDelta;
        this.moneyDelta = moneyDelta;
        this.sympathyDelta = sympathyDelta;
    }
}