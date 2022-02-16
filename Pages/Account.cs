namespace blazorbank;

public abstract class Account
{
    public Account(string name)
    {
        Name = name;
    }

    public string Name { get; }
    public decimal Balance{get;private set;}

    public virtual void MakeDeposit(decimal depositAmount)
    {
        Balance += depositAmount;
    }
}

public class SavingsAccount : Account
{
    public SavingsAccount(string name) : base(name)
    {

    }

    public decimal MaxBalance{get;set;}
}

public class CheckingAccount : Account
{
    public CheckingAccount(string name, CheckingAccountLevel accountLevel) : base(name)
    {
        AccountLevel = accountLevel;
    }

    public CheckingAccountLevel AccountLevel { get; }
}

public enum CheckingAccountLevel
{
    Standard,
    PoorBoor,
    CollegeStudent,
    HighRoller
}