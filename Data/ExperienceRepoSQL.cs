﻿using Microsoft.EntityFrameworkCore;
using MyResume.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyResume.WebApp.Data
{
    public class ExperienceRepoSQL : IExperienceRepo
    {
        private readonly AppDbContext _appDbContext;

        public ExperienceRepoSQL(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Experience CreateExp(Experience model)
        {
            _appDbContext.Experiences.Add(model);
            _appDbContext.SaveChanges();
            return model;
        }

        public ExperiencePoint CreateExpPoint(ExperiencePoint newExpPoint)
        {

            _appDbContext.Add(newExpPoint);
            _appDbContext.SaveChanges();
            return newExpPoint;
        }

        public ExperiencePointDescription CreateExpPointDesc(ExperiencePointDescription newExpPointDesc)
        {
            _appDbContext.Add(newExpPointDesc);
            _appDbContext.SaveChanges();
            return newExpPointDesc;
        }

        public Experience DeleteExp(Experience experienceToDelete)
        {
            _appDbContext.Remove(experienceToDelete);
            _appDbContext.SaveChanges();
            return experienceToDelete;
        }

        public ExperiencePoint DeleteExpPoint(ExperiencePoint experiencePointToDelete)
        {
            _appDbContext.Remove(experiencePointToDelete);
            _appDbContext.SaveChanges();
            return experiencePointToDelete;
        }

        public ExperiencePointDescription DeleteExpPointDesc(ExperiencePointDescription experiencePointDescToDelete)
        {
            _appDbContext.Remove(experiencePointDescToDelete);
            _appDbContext.SaveChanges();
            return experiencePointDescToDelete;
        }


        public Experience Read(string id)
        {
            var Experience = _appDbContext.Experiences.Include(x => x.ExperiencePoints).ThenInclude(x => x.Descriptions).FirstOrDefault(x => x.Id == id);

            foreach (var point in Experience.ExperiencePoints)
            {
                var sortedDescriptions = point.Descriptions.OrderBy(x => x.Index).ToList();
                point.Descriptions = sortedDescriptions;
            }
            Experience.ExperiencePoints = Experience.ExperiencePoints.OrderBy(x => x.Index).ToList();
            return Experience;
        }

        public IEnumerable<Experience> ReadAll(Guid userInfoId)
        {
            var result = _appDbContext.Experiences.Include(x => x.ExperiencePoints).ThenInclude(x => x.Descriptions).Where(x => x.UserInformationId == userInfoId);

            foreach (var exp in result)
            {
                var sortedExpPoints = exp.ExperiencePoints.OrderBy(x => x.Index).ToList();
                exp.ExperiencePoints = sortedExpPoints;

                foreach (var point in exp.ExperiencePoints)
                {
                    var sortedDescriptions = point.Descriptions.OrderBy(x => x.Index).ToList();
                    point.Descriptions = sortedDescriptions;
                }
            }

            result = result.OrderByDescending(x => x.Index);
            return result;
        }

        public Experience Update(Experience updatedExperience)
        {
            _appDbContext.Experiences.Update(updatedExperience);
            _appDbContext.SaveChanges();
            return updatedExperience;
        }

        public List<Experience> UpdateAll(List<Experience> updatedExperiences)
        {
            _appDbContext.Experiences.UpdateRange(updatedExperiences);
            _appDbContext.SaveChanges();
            return updatedExperiences;
        }

        public int GetExperienceCount(Guid userInfoId)
        {
            var result = _appDbContext.Experiences.Where(x => x.UserInformationId == userInfoId).ToList().Count;
            return result;
        }

    }
}
