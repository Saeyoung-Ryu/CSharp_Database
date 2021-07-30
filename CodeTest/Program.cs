using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CodeTest
{
    class Checking_Algorithm
    {       
        public static bool Check_dropDay(StockName stockName)
        {
            if(stockName.yesterday_price > stockName.current_price)
            {
                stockName.drop_day++;
                if(stockName.drop_day >= 3)
                {
                    stockName.drop_day_ok = true;
                    return true;
                }
            }

            return false;
        }

        public static bool Check_PBR_ok(StockName stockName)
        {
            if(stockName.PBR < 1)
            {
                stockName.PBR_ok = true;
                return true;
            }

            return false;
        }

        public static bool Check_Lowerthan_10years(StockName stockName)
        {
            if(stockName.Max_10years * 0.7 > stockName.current_price)
            {
                stockName.Max_10years_ok = true;
                return true;
            }

            return false;
        }

        public static bool Check_Profit(StockName stockName)
        {
            if(stockName.currentYear_netProfit > stockName.LastYear_netProfit &&  stockName.LastYear_netProfit > 0)
            {
                stockName.profit_ok = true;
                return true;
            }

            return false;
        }
    }

    class StockName
    {
        public List<bool> isokay_list = new List<bool>();
        public bool PBR_ok;
        public bool drop_day_ok;
        public bool Max_10years_ok;
        public bool profit_ok;

        public string name;
        public float PBR;
        public float Max_10years;
        public int drop_day;
        public int currentYear_netProfit;
        public int LastYear_netProfit;

        public int current_price;
        public int yesterday_price;

        public StockName(string _name, float _PBR, int _Max_10years, int _drop_day, int _current_price, int _yesterday_price, int _currentYear_netProfit, int _LastYear_netProfit)
        {
            name = _name;
            PBR = _PBR;
            Max_10years = _Max_10years;
            current_price = _current_price;
            yesterday_price = _yesterday_price;
            currentYear_netProfit = _currentYear_netProfit;
            LastYear_netProfit = _LastYear_netProfit;

            Checking_Algorithm.Check_dropDay(this);
            Checking_Algorithm.Check_Lowerthan_10years(this);
            Checking_Algorithm.Check_PBR_ok(this);
            Checking_Algorithm.Check_Profit(this);

            isokay_list.Add(PBR_ok);
            isokay_list.Add(drop_day_ok);
            isokay_list.Add(Max_10years_ok);
            isokay_list.Add(profit_ok);

            for (int i = 0; i < isokay_list.Count; i++)
            {
                if(isokay_list[i] == true)
                {
                    continue;
                }
                else
                {
                    return;
                }                
            }

            Console.WriteLine(name + " 이 조건을 충족합니다");
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            StockName hmm = new StockName()
        }
    }
}
