using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Common
{
    /// <summary>
    /// 类型查找器
    /// </summary>
    public interface ITypeFinder
    {
        /// <summary>
        /// 获取程序集
        /// </summary>
        /// <returns>程序集列表</returns>
        IList<Assembly> GetAssemblies();

        /// <summary>
        /// 查找类类型列表
        /// </summary>
        /// <param name="assignTypeFrom">继承的类型</param>
        /// <param name="onlyConcreteClasses">仅具体类</param>
        /// <returns>类型列表</returns>
        IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, bool onlyConcreteClasses = true);

        /// <summary>
        /// 查找类类型列表
        /// </summary>
        /// <param name="assignTypeFrom">继承的类型</param>
        /// <param name="assemblies">程序集列表</param>
        /// <param name="onlyConcreteClasses">仅具体类</param>
        /// <returns>类型列表</returns>
        IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true);
    }
}
