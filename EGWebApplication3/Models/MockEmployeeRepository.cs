﻿//namespace EGWebApplication3.Models
//{
//    public class MockEmployeeRepository : IEmployeeRepository
//    {
//        private readonly List<Employee> _employeeList;

//        public MockEmployeeRepository()
//        {
//            _employeeList = new List<Employee>()
//            {
//                new Employee(){Id = 101, Name="Ram", Department=Dept.HR,Email="abc@gmail.com"},
//                new Employee(){Id = 102, Name="Ravi", Department=Dept.IT,Email="xyz@gmail.com"},
//                new Employee(){Id = 103, Name="Raj", Department=Dept.IT,Email="pqr@gmail.com"},
//            };
//        }

//        public Employee GetEmployee(int id)
//        {
//            return _employeeList.FirstOrDefault(e => e.Id == id);
//        }

//        public IEnumerable<Employee> GetAllEmployees()
//        {
//            return _employeeList;
//        }

//        public Employee Add(Employee employee)
//        {
//            employee.Id = _employeeList.Max(e => e.Id) + 1;
//            _employeeList.Add(employee);
//            return employee;
//        }

//        public Employee Update(Employee employeeChanges)
//        {

//           Employee employee = _employeeList.FirstOrDefault(e => e.Id == employeeChanges.Id);
//            if (employee != null)
//            {
//                employee.Name = employeeChanges.Name;
//                employee.Email = employeeChanges.Email;
//                employee.Department = employeeChanges.Department;
//            }
//            return employee;

//        }

//        public Employee Delete(int id)
//        {
//            Employee employee = _employeeList.FirstOrDefault(e => e.Id == id);
//            if (employee != null)
//            {
//                _employeeList.Remove(employee);
//            }
//            return employee;
//        }
//    }
//}
