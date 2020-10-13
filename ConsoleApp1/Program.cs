using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

public static class Program
{
    public struct Money
    {
        public String Name;
        public Decimal Value;
        public Money(String name, Decimal value)
        {
            Name = name;
            Value = value;
        }
        public override string ToString()
        {
            return Name;
        }
    }

    static void Main()
    {
        cashRegister cr = new cashRegister();
        using (StreamReader reader = new StreamReader(Console.OpenStandardInput()))
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                Console.WriteLine(cr.giveChange(line));
            }
    }

    public class cashRegister
    {
        List<Money> cash;
        public cashRegister()
        {
            cash = new List<Money>();
            cash.Add(new Money("PENNY", 0.01m));
            cash.Add(new Money("NICKEL", 0.05m));
            cash.Add(new Money("DIME", 0.10m));
            cash.Add(new Money("QUARTER", 0.25m));
            cash.Add(new Money("HALF DOLLAR", 0.50m));
            cash.Add(new Money("ONE", 1.00m));
            cash.Add(new Money("TWO", 2.00m));
            cash.Add(new Money("FIVE", 5.00m));
            cash.Add(new Money("TEN", 10.00m));
            cash.Add(new Money("TWENTY", 20.00m));
            cash.Add(new Money("FIFTY", 50.00m));
            cash.Add(new Money("ONE HUNDRED", 100.00m));
        }

        List<String> calculateChangeMoney(Decimal change)
        {
            List<String> result = new List<String>();
            int i = cash.Count - 1;
            Decimal changeRem = change;
            while (changeRem > 0)
            {
                while ((changeRem - cash[i].Value) < 0 ){ i--; }
                result.Add(cash[i].ToString());
                changeRem = changeRem - cash[i].Value;
            }
            return result;
        }

        public String giveChange(String input)
        {
            Decimal dPP = Decimal.Parse(input.Split(';')[0]);
            Decimal dCH = Decimal.Parse(input.Split(';')[1]);
            if (dPP > dCH) { return "ERROR"; } else if (dPP == dCH) { return "ZERO"; }
            Decimal change = dCH - dPP;
            List<string> list = calculateChangeMoney(change);
            list.Sort();
            String result = "";
            list.ForEach(delegate (String element)
            {
                if (!(element.Equals(list[list.Count - 1])))
                {
                    result += (element+",");
                } else
                {
                    result += element;
                }
            });
            return result;
        }
    }
}