using CZBK.ItcastOA.DAL;
using CZBK.ItcastOA.IDAL;
using CZBK.ItcastOA.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CZBK.ItcastOA.DALFactory
{
    /// <summary>
    /// 作用1：DBSession封装了数据操作类实例的创建（起到了工厂的作用），将BLL与DAL解耦。
       // 2：提供了一个统一的数据操作的方法SaveChanges(). 
    /// </summary>
    public partial class DBSession : IDBSession
    {
       //ItcastCmsEntities Db = new ItcastCmsEntities();
       public DbContext Db
       {
           get { return DBContextFactory.CreateDbContext(); }//数据层只将数据添加到EF上并且打上相应的标记。DBSession负责与数据库进行交互。这样数据层中的EF对象和DBSession中使用的EF对象必须同一个。
       }
       //private IUserInfoDal _userInfoDal;
       //public IUserInfoDal UserInfoDal
       //{
       //    get {
       //        if (_userInfoDal == null)
       //        {
       //           // _userInfoDal = new UserInfoDal();//解耦.
       //            _userInfoDal = AbstractFactory.CreateUserInfoDal();
       //        }
       //        return _userInfoDal;
           
       //    }
       //    set { _userInfoDal = value;}
       //}

       //一个业务中有可能涉及到对多张表的操作，但是希望链接一次数据库，完成对多张表的操作。
       //工作单元模式（UnitofWork）
       public bool SaveChanges()
       {
           return Db.SaveChanges()>0;
       }
    }
}
