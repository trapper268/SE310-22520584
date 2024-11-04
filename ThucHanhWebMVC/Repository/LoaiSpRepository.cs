
using ThucHanhWebMVC.Models;
namespace ThucHanhWebMVC.Repository
{
    public class LoaiSpRepository : ILoaiSpRepository
    {
        private readonly QlbanVaLiContext _context;
        public LoaiSpRepository(QlbanVaLiContext context)
        {
            _context = context;
        }
        public TLoaiSp Add(TLoaiSp loaiSP)
        {
            _context.Add(loaiSP);
            _context.SaveChanges();
            return loaiSP;
        }

        public TLoaiSp Delete(string maloaiSP)
        {
            throw new NotImplementedException();
        }

        public TLoaiSp GetLoaiSp(string maloaiSP)
        {
            return _context.TLoaiSps.Find(maloaiSP);
        }

        public IEnumerable<TLoaiSp> GetAllLoaiSp()
        {
            return _context.TLoaiSps;
        }

        public TLoaiSp Update(TLoaiSp loaiSP)
        {
            _context.Update(loaiSP);
            _context.SaveChanges ();
            return loaiSP;
        }
    }
}
