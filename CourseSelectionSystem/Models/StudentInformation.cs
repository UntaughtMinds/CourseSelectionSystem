using CourseSelectionSystem.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseSelectionSystem.Models
{
    public class StudentInf : StudentDb
    {
        public List<String> CourseName { get; set; }
    }
    public class StudentInformation
    {
        public List<StudentInf> GetAllStudent()
        {
            List<StudentInf> res = new List<StudentInf>();
            try
            {
                using (var context = new course_selectionContext())
                {
                    var data = context.StudentDb.ToList();
                    if (data.Count() == 0)
                        throw new Exception("无学生信息");
                    foreach (var item in data)
                    {
                        var temp = new StudentInf()
                        {
                            Id = item.Id,
                            Name = item.Name,
                            Credit = item.Credit,
                            Class = item.Class
                        };
                        var tempstudent = context.StudentCourseMaping.Where(x => x.StudentId == item.Id).ToList();
                        List<String> tempName = new List<string>();
                        foreach (var i in tempstudent)
                        {
                            tempName.Add(context.CourseDb.Where(x => x.Code == i.CourseId).FirstOrDefault().Name);
                        }
                        temp.CourseName = tempName;
                        res.Add(temp);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

            return res;
        }

        public List<StudentInf> Search(int id)
        {
            List<StudentInf> res = new List<StudentInf>();
            try
            {
                using (var context = new course_selectionContext())
                {
                    var data = context.StudentDb.Where(x => x.Id == id).ToList();
                    if (data.Count() == 0)
                        throw new Exception("所查找的学生不存在");
                    foreach (var item in data)
                    {
                        var temp = new StudentInf()
                        {
                            Id = item.Id,
                            Name = item.Name,
                            Credit = item.Credit,
                            Class = item.Class
                        };
                        var tempstudent = context.StudentCourseMaping.Where(x => x.StudentId == item.Id).ToList();
                        List<String> tempName = new List<string>();
                        foreach (var i in tempstudent)
                        {
                            tempName.Add(context.CourseDb.Where(x => x.Code == i.CourseId).FirstOrDefault().Name);
                        }
                        temp.CourseName = tempName;
                        res.Add(temp);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

            return res;
        }

        public bool NewStudent(string Name, string Class)
        {
            try
            {
                using (var context = new course_selectionContext())
                {
                    var data = new StudentDb();
                    int newid = context.StudentDb.ToList().Count() == 0 ? 1 : context.StudentDb.ToList().Max(x => x.Id) + 1;
                    data.Id = newid;
                    data.Name = Name;
                    data.Class = Class;
                    context.StudentDb.Add(data);
                    context.SaveChanges();
                    Console.WriteLine("添加学生的学生编号为：" + newid);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

            return true;
        }

        public bool Change(int id, string Name, string Class)
        {
            try
            {
                using (var context = new course_selectionContext())
                {
                    var data = context.StudentDb.First(x => x.Id == id);
                    data.Name = Name;
                    data.Class = Class;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }

        public bool Delete(int id)
        {
            try
            {
                using (var context = new course_selectionContext())
                {
                    var data = context.StudentDb.Where(x => x.Id == id).FirstOrDefault();
                    context.StudentDb.Remove(data);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

            return true;
        }
    }
}
