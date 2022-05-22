using MEInsight.Entities.Core;
using MEInsight.Entities.Reference;

namespace MEInsight.Web.Extensions
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Select all children recursively of an entity in a tree like structure
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static IEnumerable<T> SelectRecursive<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> selector)
        {
            foreach (var parent in source)
            {
                yield return parent;

                var children = selector(parent);
                foreach (var child in SelectRecursive(children, selector))
                    yield return child;
            }
        }

        /// <summary>
        /// Recursive LINQ query - returns a list of related parent Organizations
        /// </summary>
        /// <param name="list">list of Organizations</param>
        /// <param name="ID">id of Child Organization</param>
        /// <returns>Hierarchical list of Organization parents</returns>
        public static IEnumerable<Organization> ListParents(IEnumerable<Organization> list, Guid? ID)
        {
            var current = list.Where(n => n.OrganizationId == ID).FirstOrDefault();
            if (current == null)
                return Enumerable.Empty<Organization>();
            return Enumerable.Concat(new[] { current }, ListParents(list, current.ParentOrganizationId));
        }

        /// <summary>
        /// Recursive LINQ query - returns a list of related parent Locations
        /// </summary>
        /// <param name="list">list of Locations</param>
        /// <param name="ID">id of Child Location</param>
        /// <returns>Hierarchical list of Location parents</returns>
        public static IEnumerable<RefLocation> ListLocations(IEnumerable<RefLocation> locations, string id)
        {
            var current = locations.Where(n => n.RefLocationId == id).FirstOrDefault();
            if (current == null)
                return Enumerable.Empty<RefLocation>();

            return Enumerable.Concat(new[] { current }, ListLocations(locations, current.ParentLocationId));
        }

    }
}
