using CourseSelectionSystem.Entitys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CourseSelectionSystem.Models
{
    public class CourseSelection
    {
        public bool select(int CourseId,int StudentId)
        {
            try
            {
                using (var context = new course_selectionContext())
                {

                    if (context.StudentDb.Where(x => x.Id == StudentId).ToList().Count() == 0)
                        throw new Exception("学生不存在");
                    if (context.CourseDb.Where(x => x.Code == CourseId).ToList().Count() == 0)
                        throw new Exception("课程不存在");
                    if (context.StudentCourseMaping.Where(x => x.CourseId == CourseId && x.StudentId == StudentId).Count() != 0)
                        throw new Exception("不允许重复选课");


                    var data = new StudentCourseMaping();
                    int id = context.StudentCourseMaping.ToList().Count() == 0 ? 1 : context.StudentCourseMaping.ToList().Max(x => x.Id) + 1;
                    data.Id = id;
                    data.StudentId = StudentId;
                    data.CourseId = CourseId;
                    context.StudentCourseMaping.Add(data);
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

        public bool Delete(int CourseId, int StudentId)
        {
            try
            {
                using (var context = new course_selectionContext())
                {
                    var data = context.StudentCourseMaping.Where(x => x.CourseId == CourseId && x.StudentId == StudentId).FirstOrDefault();
                    if (data == null)
                        throw new Exception("信息有误");
                    context.StudentCourseMaping.Remove(data);
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
