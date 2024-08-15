using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace cocult.Comands
{
    class ComandSaveJson : IComand
    {
        /// <summary>
        /// список со всеми сохраненными файлами
        /// </summary>
        private List<string> _paths;

        /// <summary>
        /// список для хранения фигур
        /// </summary>
        ListFigure<Figure> _listEnteredShapes;

        /// <summary>
        /// название команды
        /// </summary>
        public string NameComand { get; set; }

        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="paths">список сохраненных файлов</param>
        public ComandSaveJson(List<string> paths, ListFigure<Figure> _listEnteredShapes) 
        {
            NameComand = "сохранить_json";
            this._paths = paths;
            this._listEnteredShapes = _listEnteredShapes;
        }

        public async void Execute(string data)
        {
            Console.Clear();
            using (FileStream fs = new FileStream(data,FileMode.Create))
            {

                var options = new JsonSerializerOptions
                {
                    Converters = { new ShapeConverter() },
                    WriteIndented = true
                };
              
                await JsonSerializer.SerializeAsync<ListFigure<Figure>>(fs,_listEnteredShapes,options);
                 
                Console.WriteLine("Данные сохранены");
            }
            _listEnteredShapes.Clear();
        }
    }
}
