using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;

namespace Poc_ComboPlus
{
    /// <summary>
    /// Convertisseur pour créer une CollectionViewSource groupée et triée à la volée
    /// - values[O] la source de données
    /// - values[1] la propriété sur laquelle on groupe
    /// - values[2] on objet SortDescriptionCollection pour ordonner le resultat
    /// </summary>
    [ValueConversion(typeof(IEnumerable), typeof(ICollectionView))]
    public class ItemsToCollectionViewConverter : IMultiValueConverter
    {
        /// <summary>
        /// Convertis la source de donnée en CollectionViewSource
        /// </summary>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null)
            {
                throw new ArgumentNullException(nameof(values));
            }

            if (values != null && values.Length >= 1)
            {
                var collectionViewSource = new CollectionViewSource();
                collectionViewSource.Culture = culture;

                var propertyName = values[1] as string;
                if (string.IsNullOrWhiteSpace(propertyName) == false)
                {
                    collectionViewSource.GroupDescriptions.Add(new PropertyGroupDescription(propertyName));
                }

                if (values.Length == 3)
                {
                    var sortDescriptions = values[2] as SortDescriptionCollection;
                    if (sortDescriptions != null)
                    {
                        foreach (var item in sortDescriptions)
                        {
                            collectionViewSource.SortDescriptions.Add(item);
                        }
                    }
                }

                collectionViewSource.Source = values[0] as IEnumerable;

                return collectionViewSource.View;
            }

            return null;
        }

        /// <summary>
        /// Pas de conversion inverse
        /// </summary>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}