using System;
using System.Collections;

namespace com.kissmett.Common
{
	/// <summary>
	/// PreViewColumn 的摘要说明。
	/// </summary>
	public class PreViewColumn
	{
		public PreViewColumn()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		private string id;
		private int width;
		private string unit;
		private string align;
		private string fomat;
		private string caption;

		public string ID
		{
			get
			{
				return id;
			}
			set
			{
				id=value;
			}
		}

		public int Width
		{
			get
			{
				return width;
			}
			set
			{
				width=value;
			}
		}

		public string Unit
		{
			get
			{
				return unit;
			}
			set
			{
				unit=value;
			}
		}

		public string Align
		{
			get
			{
				return align;
			}
			set
			{
				align=value;
			}
		}

		public string Fomat
		{
			get
			{
				return fomat;
			}
			set
			{
				fomat=value;
			}
		}

		public string Caption
		{
			get
			{
				return caption;
			}
			set
			{
				caption=value;
			}
		}
	}

	public class PreViewColumnCollection:CollectionBase
	{
		///<summary>
		///构造函数。
		///</summary>
		public PreViewColumnCollection()
		{
			//
			//ToDo:添加构造函数逻辑。
			//
		}
	
		///<summary>
		///InventoryInfo索引器
		///</summary>
		public PreViewColumn this[int index]
		{
			get
			{
				return (PreViewColumn)List[index];
			}
			set
			{
				List[index]=value;
			}
		}

		/// <summary>
		/// 向集合中添加一个PreViewColumn对象
		/// </summary>
		/// <param name="value">一个PreViewColumn对象的实例</param>
		/// <returns>新元素在集合中的位置索引</returns>
		public int Add(PreViewColumn value)
		{
			return List.Add(value);
		}

		/// <summary>
		/// 取得特定PreViewColumn对象的索引
		/// </summary>
		/// <param name="value">一个PreViewColumn对象的实例</param>
		/// <returns>如果找到该对象，则返回其索引；否则，返回-1</returns>
		public int IndexOf(PreViewColumn value)
		{
			return List.IndexOf(value);
		}

		/// <summary>
		/// 在集合的特定位置上插入一个PreViewColumn对象
		/// </summary>
		/// <param name="value">一个PreViewColumn对象的实例</param>
		/// <param name="index">从0开始的索引值，在该位置处插入一个对象</param>
		public void Insert(PreViewColumn value,int index)
		{
			List.Insert(index,value);
		}

		/// <summary>
		/// 从集合中删除一个PreViewColumn对象
		/// </summary>
		/// <param name="value">一个PreViewColumn对象的实例</param>
		public void Remove(PreViewColumn value)
		{
			List.Remove(value);
		}

		/// <summary>
		/// 判断集合中是否存在特定PreViewColumn对象
		/// </summary>
		/// <param name="value">一个PreViewColumn对象的实例</param>
		/// <returns>如果存在该实例，则返回true;否则返回false</returns>
		public bool Contains(PreViewColumn value)
		{
			return List.Contains(value);
		}
	}
}
