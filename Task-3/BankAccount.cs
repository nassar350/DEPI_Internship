using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_3
{
    public class BankAccount
    {
        const string Bank_code = "BNK001";
        readonly DateTime Created_date;
        readonly int AccountNumber;

        private int _Phonenumber;
        private string _NationalID;
        private string _Fullname;
        private string _Address;
        private decimal _Balance;

        public string Fullname
        {
            get
            {
                return _Fullname;
            }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    Console.WriteLine("Invalid Name");
                }
                else
                {
                    _Fullname = value;
                }
            }
        }

        public string NationalID
        {
            get
            {
                return _NationalID;
            }
            set
            {
                if (value.Length == 14)
                {
                    Console.WriteLine("Invalid National ID");
                }
                else
                {
                    _NationalID = value;
                }
            }
        }

        public int Phonenumber
        {
            get
            {
                return _Phonenumber;
            }
            set
            {
                string number = Convert.ToString(value);
                if (number.Length == 11 && number[0] == '0' && number[1] == '1')
                {
                    _Phonenumber = value;
                }
                else
                {
                    Console.WriteLine("Invalid Phone Number");
                }
            }
        }

        public decimal Balance
        {
            get
            {
                return _Balance;
            }
            set
            {
                if (value >= 0)
                {
                    _Balance = value;
                }
                else
                {
                    Console.WriteLine("Invalid Balance Must Be Positive");
                }
            }
        }

        public string Address
        {
            get
            {
                return _Address;
            }
            set
            {
                if (value.Length == 0)
                {
                    Console.WriteLine("Enter your Address");
                }
                else
                {
                    _Address = value;
                }
            }
        }


        public BankAccount()
        {
            _Balance = 0;
            _Fullname = "user";
            _NationalID = "no ID";
            _Phonenumber = 0;
            _Address = "no address";
            AccountNumber = 0;
            Created_date = DateTime.Now;
        }
        public BankAccount(string full_name, string nationalID, int phonenumber, string address, int accountnumber)
        {
            _Fullname = full_name;
            _NationalID = nationalID;
            _Phonenumber = phonenumber;
            _Address = address;
            _Balance = 0;
            AccountNumber = accountnumber;
            Created_date = DateTime.Now;
        }

        public BankAccount(string full_name, string nationalID, int phonenumber, string address, int accountnumber, int balance) : this(full_name, nationalID, phonenumber, address, accountnumber)
        {
            _Balance = balance;

            Created_date = DateTime.Now;
        }


        public virtual void showAccountDetails()
        {
            Console.WriteLine($"name: {Fullname},\n"+
                $"phone: {Phonenumber},\n"+
                $"balance: {Balance} EGP,\n"+
                $"address: {Address},\n"+
                $"national ID: {NationalID},\n"+
                $"Creation Date: {Created_date}");
        }

        bool isValidNationalID()
        {
            if (_NationalID.Length == 14)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        bool isValidPhoneNumber()
        {
            string pnumber = Convert.ToString(_Phonenumber);
            if (pnumber.Length == 11 && pnumber[0] == '0' && pnumber[1] == '1')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public virtual decimal CalculateInterest()
        {
            return 0;
        }
    }
}
