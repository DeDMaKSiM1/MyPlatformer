using System;
using System.Linq;
using UnityEngine;
namespace MyPlatform.Components
{
    public class SpawnListComponent : MonoBehaviour
    {
        //Будет агрегировать несколько спавн-компонентов, которые мы будем вызывать по их имени
        //При этом агрегировать скрипт будет не в переменные, а в массив специальных классов SpawnData
        [SerializeField] private SpawnData[] _spawners;



        //Создадим метод Spawn, который будет получать то, что мы хотим заспавнить
        public void Spawn(string id)
        {
            /*
            foreach(var data in _spawners)
            {
                if(data.id == id)
                {
                    data.Component.Spawn();
                    break;
                }
            }
            Делает то же самое, что и ниже приведенное
            *///Другая реализация, более длинная
            //Воспользуемся библиотекой Linq и передаем в метод предикат в (). Предикат - функция в сокращенном виде, будет
            //вызываться на каждом элементе _spawners и в эту функцию будет передавать каждый элемент(element) и для каждого
            //элемента будет проверка если id этого element равен id, которое мы передали, то мы вернем данный элемент массива
            var spawner = _spawners.FirstOrDefault(element => element.id = id);
            spawner?.Component.Spawn();//Спрашиваем через запрос(?.), т.к. spawner может быть = null
            //Если пишем горячий код(который вызывается в Update например) лучше не использовать конструкцию Linq, т.к. он может
            //генерить мусор, который плохо повлияет на оптимизацию.
        }
        //Сделаем вложенный класс, где сделаем также, как в клипах??
        [Serializable]
        public class SpawnData
        {
            public string id;
            public SpawnComponent Component;

        }
    }

}

