namespace SwitchConst
{
    internal class Cpr
    {
        //Field
        DateTime date;
        public Cpr()
        {
            while (!CheckCpr(GetCpr()))
            {
                Console.WriteLine("Forkert input, prøv igen");
            };
            //TODO Format date
            Console.WriteLine("Fødselsdato : " + date.ToString("dd/MM-yyyy"));
        }
        string? GetCpr()
        {
            Console.Write("Indtast CPR nr. (ddmmyy-xxxx):");
            string? input = Console.ReadLine();
            return input;
        }
        bool CheckCpr(string? cpr)
        {
            //Check if cpr is null or length doesnt match or 7th position is dash
            if (cpr == null || cpr.Length != 11 || cpr[6] != '-') return false;

            //tryparse the first 6 char in string for datetime
            if (!IsDate(cpr)) return false;

            //Is last 4 digits?
            foreach (char c in cpr.Substring(7, 4))
                if (c < '0' || c > '9') return false;

            //Is the CPR valid (modulus 11 check)
            if (!CheckModulus11(cpr)) return false;

            //All is well, thank god!
            return true;
        }
        private bool CheckModulus11(string cpr)
        {
            //if CPR is after 2007, dont check for modulus11
            if (date > new DateTime(2007, 10, 1)) return true;

            int m = 0;
            int[] modifiers = new int[10] { 4, 3, 2, 7, 6, 5, 4, 3, 2, 1 };
            string newCpr = cpr.Remove(6,1);
            for (int i = 0; i < 10; i++)
            {
                m += modifiers[i] * int.Parse(newCpr[i].ToString());
            }
            if (m % 11 != 0) return false;
            return true;
        }
        private bool IsDate(string cpr)
        {
            return DateTime.TryParseExact(
                cpr.Substring(0, 6),
                "ddMMyy",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None,
                out date);
        }
    }
}
