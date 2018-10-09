using System;
using System.Collections.Generic;

namespace Hska_Grades_Tool
{
    static class MockGradesService
    {
        static Random random = new Random();

        static public List<ExamGrades> getGrades(string username, string password)
        {
            List<ExamGrades> examGradesList = new List<ExamGrades>();
            for (int i = 0; i < 10; i++)
            {
                examGradesList.Add(getMockExamGrades(i));
            }
            return examGradesList;
        }

        static public ExamGrades getMockExamGrades(int id)
        {
            ExamGrades examGrades = new ExamGrades();
            examGrades.ExamId = "TestID"+id;
            examGrades.ExamTitle = "TestTitle" + id;
            examGrades.Grade = random.Next(100, 500);
            examGrades.Status = examGrades.Grade <= 4.0 ? "BE" : "NB";
            examGrades.Semester = random.NextDouble() > 0.5 ? "20141" : "20142";
            examGrades.Attempts = random.Next(1, 3);

            // Random date
            DateTime start = new DateTime(2014, 1, 1);
            int range = 300;
            DateTime randomDay = start.AddDays(random.Next(range));
            examGrades.ExamDate = randomDay.ToString().Substring(0, 10);

            examGrades.G100 = random.Next(0, 12);
            examGrades.G130 = random.Next(0, 12);
            examGrades.G170 = random.Next(0, 12);
            examGrades.G200 = random.Next(0, 12);
            examGrades.G230 = random.Next(0, 12);
            examGrades.G270 = random.Next(0, 12);
            examGrades.G300 = random.Next(0, 12);
            examGrades.G330 = random.Next(0, 12);
            examGrades.G370 = random.Next(0, 12);
            examGrades.G400 = random.Next(0, 12);
            examGrades.G430 = random.Next(0, 12);
            examGrades.G470 = random.Next(0, 12);
            examGrades.G500 = random.Next(0, 12);

            examGrades.Participants += examGrades.G100;
            examGrades.Participants += examGrades.G130;
            examGrades.Participants += examGrades.G170;
            examGrades.Participants += examGrades.G200;
            examGrades.Participants += examGrades.G230;
            examGrades.Participants += examGrades.G270;
            examGrades.Participants += examGrades.G300;
            examGrades.Participants += examGrades.G330;
            examGrades.Participants += examGrades.G370;
            examGrades.Participants += examGrades.G400;
            examGrades.Participants += examGrades.G430;
            examGrades.Participants += examGrades.G470;
            examGrades.Participants += examGrades.G500;
            
            return examGrades;
        }
    }
}
