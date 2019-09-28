﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Cosolem
{
    public class SortableBindingList<T> : BindingList<T>
    {
        List<T> originalList;
        ListSortDirection sortDirection;
        PropertyDescriptor sortProperty;

        Action<SortableBindingList<T>, List<T>> populateBaseList = (a, b) => a.ResetItems(b);

        static Dictionary<string, Func<List<T>, IEnumerable<T>>> cachedOrderByExpressions = new Dictionary<string, Func<List<T>, IEnumerable<T>>>();

        public SortableBindingList()
        {
            originalList = new List<T>();
        }

        public SortableBindingList(IEnumerable<T> enumerable)
        {
            originalList = enumerable.ToList();
            populateBaseList(this, originalList);
        }

        public SortableBindingList(List<T> list)
        {
            originalList = list;
            populateBaseList(this, originalList);
        }

        protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
        {
            sortProperty = prop;

            var orderByMethodName = sortDirection == ListSortDirection.Ascending ? "OrderBy" : "OrderByDescending";
            var cacheKey = typeof(T).GUID + prop.Name + orderByMethodName;

            if (!cachedOrderByExpressions.ContainsKey(cacheKey))
                CreateOrderByMethod(prop, orderByMethodName, cacheKey);

            ResetItems(cachedOrderByExpressions[cacheKey](originalList).ToList());
            ResetBindings();
            sortDirection = sortDirection == ListSortDirection.Ascending ? ListSortDirection.Descending : ListSortDirection.Ascending;
        }

        private void CreateOrderByMethod(PropertyDescriptor prop, string orderByMethodName, string cacheKey)
        {
            var sourceParameter = Expression.Parameter(typeof(List<T>), "source");
            var lambdaParameter = Expression.Parameter(typeof(T), "lambdaParameter");
            var accesedMember = typeof(T).GetProperty(prop.Name);
            var propertySelectorLambda = Expression.Lambda(Expression.MakeMemberAccess(lambdaParameter, accesedMember), lambdaParameter);
            var orderByMethod = typeof(Enumerable).GetMethods().Where(a => a.Name == orderByMethodName && a.GetParameters().Length == 2).Single().MakeGenericMethod(typeof(T), prop.PropertyType);

            var orderByExpression = Expression.Lambda<Func<List<T>, IEnumerable<T>>>(Expression.Call(orderByMethod, new Expression[] { sourceParameter, propertySelectorLambda }), sourceParameter);

            cachedOrderByExpressions.Add(cacheKey, orderByExpression.Compile());
        }

        protected override void RemoveSortCore()
        {
            ResetItems(originalList);
        }

        private void ResetItems(List<T> items)
        {
            base.ClearItems();
            for (int i = 0; i < items.Count; i++)
                base.InsertItem(i, items[i]);
        }

        protected override bool SupportsSortingCore
        {
            get
            {
                return true;
            }
        }

        protected override ListSortDirection SortDirectionCore
        {
            get
            {
                return sortDirection;
            }
        }

        protected override PropertyDescriptor SortPropertyCore
        {
            get
            {
                return sortProperty;
            }
        }

        protected override void OnListChanged(ListChangedEventArgs e)
        {
            originalList = base.Items.ToList();
        }
    }
}