﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cau1.DTO
{
    public class DepartmentDTO
    {
        public string IdDepartment { get; set; }
        public string Name { get; set; }
        public List<EmployeeDTO> Employees { get; set; }
    }
}
