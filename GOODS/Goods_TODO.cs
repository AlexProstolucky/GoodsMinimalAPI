namespace Goods
{
    public class Goods_TODO
    {
        List<Good> goods;
        public Goods_TODO(List<Good> goods)
        {
            this.goods = goods;
        }
        public void Add(Good good)
        {
            goods.Add(good);
        }

        public Good Get(int goodId)
        {
            foreach (Good good in goods)
            {
                if (good.id == goodId.ToString())
                {
                    return good;
                }
            }
            return new Good(null, null, null);
        }

        public bool Is(string Id)
        {
            foreach (Good good in goods)
            {
                if (good.id == Id)
                {
                    return true;
                }
            }
            return false;
        }
        public void Remove(string Id)
        {
            foreach (Good good in goods)
            {
                if (good.id == Id)
                {
                    goods.Remove(good);
                }
            }
        }
        public void Update(string Id, string name, string description, string path)
        {
            foreach (Good good in goods)
            {
                if (good.id == Id)
                {
                    good.name = name;
                    good.description = description;
                    good.image_path = path;
                }
            }
        }
        public List<Good> GetGoods()
        {
            return goods;
        }
    }
}
