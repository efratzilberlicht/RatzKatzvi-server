using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dto;
namespace BL
{
    public class SearchesBL
    {
        public static IEnumerable<WordLocation1> SearchText(string text)
        {
            List<WordLocation1> result = new List<WordLocation1>();
            List<Subjects1> subjects = new List<Subjects1>();
            List<PreSerches1> presearches = new List<PreSerches1>();
            List<int> subjectIds = new List<int>();
            List<int> presearcheIds = new List<int>();
            presearches = PreSerchesBL.GetWordIdByName(text);
            subjects.Add(SubjectsBL.GetSubjectByName(text));
            subjects.AddRange(SubjectsBL.GetSubjectContainText(text));
            presearcheIds.AddRange(presearches.Select(pre => pre.Id));
            subjectIds.AddRange(subjects.Select(subject => subject.SubjectId).ToList());
            result = WordLocationBL.GetAll().Where(w => subjectIds.Contains(w.SubjectId ?? 0) || presearcheIds.Contains(w.SearchId ?? 0)).ToList();
            if (presearcheIds.Count == 0)
            {
                result.AddRange(Convertors.WordLocationConvertor.ConvertToListDto(DL.SearchesDL.SearchInBooks(text)));
            }
            foreach (var preSearch in presearches)
            {
                preSearch.SearchedCounter++;
                PreSerchesBL.UpdatePreSerch(preSearch);
            }
            foreach (var subject in subjects)
            {
                subject.SearchedCounter++;
                SubjectsBL.UpdateSubject(subject);
            }
            return result.OrderBy(b => subjectIds.IndexOf(b.SubjectId ?? 0)).ThenBy(b => presearcheIds.IndexOf(b.SearchId ?? 0));
        }
    }
}
