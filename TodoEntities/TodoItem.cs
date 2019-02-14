using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TodoEntities
{
    /// <summary>
    /// Задача
    /// </summary>
    public class TodoItem
    {
        public long Id { get; set; }

        /// <summary>
        /// Задача
        /// </summary>
        [DisplayName("Задача")]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Статус выполнения
        /// </summary>
        [DisplayName("Выполнена")]
        public bool IsComplete { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        [DisplayName("Создана")]
        public DateTime Created { get; set; }
    }
}
