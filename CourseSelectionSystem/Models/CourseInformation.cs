using CourseSelectionSystem.Entitys;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseSelectionSystem.Models
{
    public class CourseInf : CourseDb
    {
        public List<String> StudentName { get; set; }
    }
    public class CourseInformation
    {
        public List<CourseInf> GetAllCourse()
        {
            List<CourseInf> res = new List<CourseInf>();
            using (var context = new course_selectionContext())
            {
                try
                {
                    var data = context.CourseDb.ToList();
                    if (data.Count() == 0)
                        throw new Exception("无课程");
                    foreach (var item in data)
                    {
                        var temp = new CourseInf()
                        {
                            Code = item.Code,
                            Credit = item.Credit,
                            IsElective = item.IsElective,
                            Name = item.Name
                        };
                        var tempstudent = context.StudentCourseMaping.Where(x => x.CourseId == item.Code).ToList();
                        List<String> tempName = new List<string>();
                        foreach (var i in tempstudent)
                        {
                            tempName.Add(context.StudentDb.Where(x => x.Id == i.StudentId).FirstOrDefault().Name);
                        }
                        temp.StudentName = tempName;
                        res.Add(temp);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
            return res;
        }

        public List<CourseInf> Search(int id)
        {
            List<CourseInf> res = new List<CourseInf>();
            using (var context = new course_selectionContext())
            {
                try
                {
                    var data = context.CourseDb.Where(x => x.Code == id).ToList();
                    if (data.Count() == 0)
                        throw new Exception("所查找的课程不存在");
                    foreach (var item in data)
                    {
                        var temp = new CourseInf()
                        {
                            Code = item.Code,
                            Credit = item.Credit,
                            IsElective = item.IsElective,
                            Name = item.Name
                        };
                        var tempstudent = context.StudentCourseMaping.Where(x => x.CourseId == item.Code).ToList();
                        List<String> tempName = new List<string>();
                        foreach (var i in tempstudent)
                        {
                            tempName.Add(context.StudentDb.Where(x => x.Id == i.StudentId).FirstOrDefault().Name);
                        }
                        temp.StudentName = tempName;
                        res.Add(temp);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
            return res;
        }

        public bool NewCourse(string Name,int IsElective,int Credit)
        {
            try
            {
                using (var context = new course_selectionContext())
                {
                    var data = new CourseDb();
                    int newid = context.CourseDb.ToList().Count() == 0 ? 1 : context.CourseDb.ToList().Max(x => x.Code) + 1;
                    data.Code = newid;
                    data.Name = Name;
                    data.IsElective = IsElective;
                    data.Credit = Credit;
                    context.CourseDb.Add(data);
                    context.SaveChanges();
                    Console.WriteLine("新课程的课程编号为：" + newid);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }

            return true;
        }

        public bool Change(int id, string Name, int IsElective, int Credit)
        {
            try
            {
                using (var context = new course_selectionContext())
                {
                    var data = context.CourseDb.First(x => x.Code == id);
                    data.Credit = Credit;
                    data.IsElective = IsElective;
                    data.Name = Name;
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
                    var data = context.CourseDb.Where(x => x.Code == id).FirstOrDefault();
                    if (data.IsElective == 1 && data.StudentCourseMaping != null)
                        throw new Exception("已有学生选修该选修课程，无法删除");
                    context.CourseDb.Remove(data);
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
