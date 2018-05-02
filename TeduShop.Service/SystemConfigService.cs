using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduShop.Data.Inframestructure;
using TeduShop.Data.Reponsitories;
using TeduShop.Model.Models;

namespace TeduShop.Service
{
    public interface ISystemConfigService
    {
        IEnumerable<SystemConfig> GetAll();
        SystemConfig Detail(int id);
        SystemConfig GetByCode(string code);
        void Delete(int id);
        void Update(SystemConfig systemConfig);
        void Add(SystemConfig systemConfig);
        void SaveChange();
    }
    public class SystemConfigService : ISystemConfigService
    {
        private ISystemConfigRepository _systemConfigRepository;
        private IUnitOfWork _unitOfWork;
        public SystemConfigService(ISystemConfigRepository systemConfigRepository,IUnitOfWork unitOfWork)
        {
            this._systemConfigRepository = systemConfigRepository;
            this._unitOfWork = unitOfWork;
        }
        public void Add(SystemConfig systemConfig)
        {
            _systemConfigRepository.Add(systemConfig);
        }

        public void Delete(int id)
        {
            _systemConfigRepository.Delete(id);
        }

        public SystemConfig Detail(int id)
        {
            return _systemConfigRepository.GetSingleById(id);
        }

        public IEnumerable<SystemConfig> GetAll()
        {
            return _systemConfigRepository.GetAll();
        }

        public SystemConfig GetByCode(string code)
        {
            return _systemConfigRepository.GetSingleByCondition(x => x.Code == code);
        }

        public void SaveChange()
        {
            _unitOfWork.Commit();
        }

        public void Update(SystemConfig systemConfig)
        {
            _systemConfigRepository.Update(systemConfig);
        }
    }
}
