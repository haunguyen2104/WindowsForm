﻿using Cau1.BLL;
using Cau1.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cau1
{
    public partial class Employee : Form
    {
        EmployeeBLL empBLL = new EmployeeBLL();
        DepartmentBLL depBLL = new DepartmentBLL();
        public Employee()
        {
            InitializeComponent();
        }

        private void Employee_Load(object sender, EventArgs e)
        {


            List<EmployeeDTO> lstEmp = empBLL.ReadEmployee();
            foreach (EmployeeDTO emp in lstEmp)
            {

                dataView.Rows.Add(emp.IdEmployee,
                                                    emp.Name,
                                                    emp.DateBirth,
                                                    emp.Gender,
                                                    emp.PlaceBirth,
                                                    emp.DepartmentName);
            }
            List<DepartmentDTO> lstDep = depBLL.ReadDepartmentList();

            foreach (DepartmentDTO dep in lstDep)
            {
                cbDepartment.Items.Add(dep);
            }
            cbDepartment.DisplayMember = "NameDep";
        }

        private void dataView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            int index = e.RowIndex;
            DataGridViewRow row = dataView.Rows[index];
            if (row.Cells[0].Value != null)
            {
                tbID.Text = row.Cells[0].Value.ToString();
                tbName.Text = row.Cells[1].Value.ToString();
                dtDateBirth.Text = row.Cells[2].Value.ToString();
                if (row.Cells[3].Value.ToString() != "Nam")
                {
                    ckbGender.Checked = false;
                }
                else
                {
                    ckbGender.Checked = true;
                }
                tbPlaceBirth.Text = row.Cells[4].Value.ToString();
                cbDepartment.Text = row.Cells[5].Value.ToString();

                tbID.Enabled = false;
                btnAdd.Enabled = false;

            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            EmployeeDTO emp = new EmployeeDTO();
            emp.IdEmployee = int.Parse(tbID.Text);
            emp.Name = tbName.Text;
            emp.DateBirth = dtDateBirth.Value;
            if (ckbGender.Checked)
            {
                emp.Gender = "Nam";
            }
            else
            {
                emp.Gender = "Nữ";
            }
            emp.PlaceBirth = tbPlaceBirth.Text;
            emp.Departments = (DepartmentDTO)cbDepartment.SelectedItem;



            //-------------
            //List<EmployeeDTO> lstEmp = empBLL.ReadEmployee();
            //foreach (EmployeeDTO empl in lstEmp)
            //{

            //    empl.IdEmployee.Find
            //}
            //Ràng buộc dữ liệu 

            if (cbDepartment.Text == "") { MessageBox.Show("Bạn chưa chọn đơn vị.", "Thông báo"); }
            else
            {
                if (tbID.Text == "") { MessageBox.Show("Không được để trống ID", "Thông báo"); }
                else
                {
                    if (tbName.Text == "") { MessageBox.Show("Không được để trống Tên", "Thông báo"); }
                    else
                    {
                        if (tbPlaceBirth.Text == "") { MessageBox.Show("Không được để trống Nơi sinh", "Thông báo"); }
                        else
                        {
                            empBLL.NewEmployee(emp);
                            dataView.Rows.Add(emp.IdEmployee,
                                                                    emp.Name,
                                                                    emp.DateBirth,
                                                                    emp.Gender,
                                                                    emp.PlaceBirth,
                                                                    emp.DepartmentName);
                            Clear();
                        }
                    }
                }

                //End ràng buộc
            }



        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            EmployeeDTO emp = new EmployeeDTO();
            emp.IdEmployee = int.Parse(tbID.Text);

            var result = MessageBox.Show("Bạn có  chắc chắn muốn xóa nhân viên này không? ", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                empBLL.DeleteEmployee(emp);
                int index = dataView.CurrentCell.RowIndex;
                dataView.Rows.RemoveAt(index);
                MessageBox.Show("Xóa nhân viên thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataView.CurrentRow;
            if (row != null)
            {
                EmployeeDTO emp = new EmployeeDTO();
                emp.IdEmployee = int.Parse(tbID.Text);
                emp.Name = tbName.Text;
                emp.DateBirth = dtDateBirth.Value;
                if (ckbGender.Checked)
                {
                    emp.Gender = "Nam";
                }
                else
                {
                    emp.Gender = "Nữ";
                }
                emp.PlaceBirth = tbPlaceBirth.Text;
                emp.Departments = (DepartmentDTO)cbDepartment.SelectedItem;

                //Ràng buộc dữ liệu 

                if (cbDepartment.Text == "") { MessageBox.Show("Bạn chưa chọn đơn vị.", "Thông báo"); }
                else
                {
                    if (tbName.Text == "") { MessageBox.Show("Không được để trống Tên", "Thông báo"); }
                    else
                    { 
                        if (tbPlaceBirth.Text == "") { MessageBox.Show("Không được để trống Nơi sinh", "Thông báo"); }
                        else
                        {
                            empBLL.EditEmployee(emp);

                            row.Cells[0].Value = emp.IdEmployee;
                            row.Cells[1].Value = emp.Name;
                            row.Cells[2].Value = emp.DateBirth;
                            row.Cells[3].Value = emp.Gender;
                            row.Cells[4].Value = emp.PlaceBirth;
                            row.Cells[5].Value = emp.DepartmentName;
                        }
                    }
                } //End ràng buộc

            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Bạn có muốn đóng chương trình không ? ", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                this.Close();
            }
        }
        private void Clear()
        {
            tbID.Enabled = true;
            tbID.Text = "";
            tbName.Text = "";
            dtDateBirth.Value = DateTime.Now;
            ckbGender.Checked = false;
            cbDepartment.Text = "";
            tbPlaceBirth.Text = "";
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            tbID.Enabled = true;
            tbID.Text = "";
            tbName.Text = "";
            dtDateBirth.Value = DateTime.Now;
            ckbGender.Checked = false;
            cbDepartment.Text = "";
            tbPlaceBirth.Text = "";
            btnAdd.Enabled = true;


        }
    }
}
