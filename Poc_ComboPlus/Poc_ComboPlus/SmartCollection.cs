using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;

namespace Poc_ComboPlus
{
    public class SmartCollection<T> : ObservableCollection<T>
    {
        public SmartCollection()
            : base()
        {
        }

        public SmartCollection(IEnumerable<T> collection)
            : base(collection)
        {
        }

        public SmartCollection(IList<T> list)
            : base(list)
        {
        }

        /// <summary>
        /// Ajout d'un ensemble d'éléments dans la liste
        /// </summary>
        /// <param name="range"></param>
        public void AddRange(IEnumerable<T> range)
        {
            if (range != null && range.Any())
            {
                foreach (var item in range)
                {
                    Items.Add(item);
                }

                this.OnPropertyChanged(new PropertyChangedEventArgs("Count"));
                this.OnPropertyChanged(new PropertyChangedEventArgs("Item[]"));
                this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        /// <summary>
        /// Réinitialise la liste en remplaçant son contenu par la liste passée en paramètre
        /// </summary>
        /// <param name="range">Nouveau contenu de la liste à l'issue de la réinitialisation</param>
        public void Reset(IEnumerable<T> range)
        {
            this.Items.Clear();

            AddRange(range);
        }
    }
}