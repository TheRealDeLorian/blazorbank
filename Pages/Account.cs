namespace blazorbank;

public abstract class Account
{
    public Account(string name)
    {
        Name = name;
    }

    public string Name { get; }
    public decimal Balance { get; private set; }

    public virtual void MakeDeposit(decimal depositAmount)
    {
        Balance += depositAmount;
    }

    public virtual void Save(StreamWriter writer)
    {
        writer.WriteLine("Name:" + this.Name);
        writer.WriteLine("Balance:" + this.Balance);
    }

    public static List<Account> Load(string path)
    {
        var accounts = new List<Account>();
        var accountName = "";
        var balance = 0M;
        CheckingAccountLevel accountLevel = CheckingAccountLevel.Standard;
        foreach (var line in File.ReadAllLines(path))
        {
            var parts = line.Split(':');
            if (parts[0] == "Name")
            {
                accountName = parts[1];
            }
            else if (parts[0] == "Balance")
            {
                balance = decimal.Parse(parts[1]);
            }
            else if (parts[0] == "AccountLevel")
            {
                accountLevel = (CheckingAccountLevel)Enum.Parse(typeof(CheckingAccountLevel), parts[1]);
            }
            else if (parts[0] == "AccountType")
            {
                switch (parts[1])
                {
                    case "SavingsAccount":
                        accounts.Add(new SavingsAccount(accountName));
                        break;
                    case "CheckingAccount":
                        accounts.Add(new CheckingAccount(accountName, accountLevel));
                        break;
                }
            }
        }

        return accounts;
    }
}

public class SavingsAccount : Account
{
    public SavingsAccount(string name) : base(name)
    {

    }

    public decimal MaxBalance { get; set; }

    public override void Save(StreamWriter writer)
    {
        base.Save(writer);
        writer.WriteLine("MaxBalance:" + MaxBalance);
        writer.WriteLine("AccountType:SavingsAccount");
    }
}

public class CheckingAccount : Account
{
    public CheckingAccount(string name, CheckingAccountLevel accountLevel) : base(name)
    {
        AccountLevel = accountLevel;
    }

    public CheckingAccountLevel AccountLevel { get; }

    public override void Save(StreamWriter writer)
    {
        base.Save(writer);
        writer.WriteLine("AccountLevel:" + AccountLevel);
        writer.WriteLine("AccountType:CheckingAccount");
    }
}

public enum CheckingAccountLevel
{
    Standard,
    PoorBoor,
    CollegeStudent,
    HighRoller
}