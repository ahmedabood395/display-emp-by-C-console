using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;

namespace labday2
{
    enum Gender
    {
        none,Male, Female
    }
    [Flags]
    enum securityprivileges : byte
    {
        none = 0, guest = 1, Developer = 2, secretary = 4, DBA = 8, ScurityOfficer=16

    }
    class HiringDate:IComparable
    {
        public int day { get; set; }
        public int month { get; set; }
        public int year { get; set; }
        public HiringDate(int day=0, int month=0, int year=0)
        {
            this.day = day;
            this.month = month;
            this.year = year;
        }

        public override string ToString()
        {
            return $"Day={day},Month={month},Year={year}";
        }

        public int CompareTo(object obj)
        {
            HiringDate h = obj as HiringDate;
            if (h!=null)
            {
                int x = day.CompareTo(h.day);
                if(x==0)
                {
                    return month.CompareTo(h.month);
                }
                else
                {
                    return x;
                }
            }
            else
            {
                return -2;
            }
        }
    }
    class Employee:IComparable
    {
        public int id { get; set; }
        public securityprivileges security_level { get; set; }
        public int salary { get; set; }
        public HiringDate hire_date { get; set; }
        public Gender gender { get; set; }
        public Employee(int id, securityprivileges security_level, int salary, HiringDate hire_date, Gender gender)
        {
            this.id = id;
            this.security_level = security_level;
            this.salary = salary;
            this.hire_date = hire_date;
            this.gender = gender;
        }
        public Employee()
        {
            id = 0;
            salary = 0;
            security_level = securityprivileges.none;
            hire_date = new HiringDate();
            gender = Gender.none;
        }
        public override string ToString()
        {
            return $"Id={id},Security={security_level},Salary={salary:c},Gender={gender},\nDate.....\n{hire_date.ToString()}";
        }

        public override bool Equals(object obj)
        {
            Employee em = (Employee)obj;

            return (id == em.id && salary == em.salary && hire_date == em.hire_date && security_level == em.security_level && gender == em.gender);
        }

        public int CompareTo(object obj)
        {
            Employee emp = obj as Employee;
            if (emp!=null)
            {
                int x = hire_date.CompareTo(emp.hire_date);
                if (x==0)
                {
                    return id.CompareTo(emp.id);
                }
                else
                {
                    return x;
                }
                //int x = hire_date.day.CompareTo(emp.hire_date.day);
                //if(x==0)
                //{
                //    return hire_date.month.CompareTo(emp.hire_date.month);
                //}
                //else
                //{
                //    return x;
                //}
            }
            else
            {
                return -2;
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            
            //exError e = new exError();
            HiringDate dat = new HiringDate(3, 11, 1994);



            Employee emp = new Employee(1, securityprivileges.DBA, 5000, dat, Gender.Male);
            Employee emp2 = new Employee(1, securityprivileges.DBA, 5000, dat, Gender.Male);


            if (emp.Equals(emp2))
            {
                Console.WriteLine("emp1=emp2");
            }
            try
            {
                #region Array
                Employee[] Emparr = new Employee[3];

                for (int i = 0; i < Emparr.Length; i++)
                {
                    Console.Write("Day=");
                    int day = int.Parse(Console.ReadLine());
                    Console.Write("Month=");
                    int month = int.Parse(Console.ReadLine());
                    Console.Write("Year=");
                    int year = int.Parse(Console.ReadLine());
                    HiringDate dt = new HiringDate(day, month, year);
                    Console.Write("ID=");
                    int id = int.Parse(Console.ReadLine());

                    securityprivileges ScurityOfficer = securityprivileges.DBA | securityprivileges.Developer | securityprivileges.guest | securityprivileges.secretary;

                    Console.Write("Enter Your Security guest=1, Developer=2, secretary=4 , DBA=8,ScurityOfficer=16");
                    securityprivileges s = (securityprivileges)Enum.Parse(typeof(securityprivileges), Console.ReadLine());
                    if (s == securityprivileges.ScurityOfficer)
                    {
                        s = ScurityOfficer;
                    }

                    Console.Write("Salary=");
                    int salary = int.Parse(Console.ReadLine());

                    Console.WriteLine("Enter Your Gender  Male=1 or Female=2");
                    Gender g = (Gender)Enum.Parse(typeof(Gender), Console.ReadLine());
                    Emparr[i] = new Employee(id, s, salary, dt, g);


                    
                }
                Array.Sort(Emparr);
                foreach (Employee item in Emparr)
                {
                    Console.WriteLine(item.ToString());
                }

                #endregion

            }
            catch(Exception ex)
            {
                
                StreamWriter sr = new StreamWriter("Error.txt");
                sr.Write(ex.Message);
                sr.Close();
                Console.WriteLine(ex.Message);
            }


        }
    }
}
