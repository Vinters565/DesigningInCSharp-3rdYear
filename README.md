# DesigningInCSharp-3rdYear

 ## Идея проекта (Приложение-календарь для составление расписаний)
 Проект представит десктопное приложение на Windows и будет реализован в виде обычного календаря, в котором можно будет создавать события и присваивать им различные атрибуты.
 
 Особенности проекта:
 1. Календарь предполагает несколько фильтров для фильтрации и сортировки событий по их атрибутам.
 2. Проект включит в себя систему регистрации и личного кабинета.
 3. Будет возможность создания личного и публичного календаря, что даст возможность создания личных и публичных событий.
 4. Публичные события можно будет отслеживать в личном расписании, если подписаться на них.
 5. Будет представлен свод правил для создания и добавления событий на основе их атрибутов(так например 2 обязательных события зависящих от места нельзя будет добавить на одну дату и пересекающееся время).

 Сценарий использования:
 1. Пользователь скачал приложение и попал на страницу входа;
 2. Зарегистрировался, без подтверждения где-либо, вошёл в свой аккаунт;
 3. У пользователя 3 функциональных вкладок: Личный кабинет, Личный календарь, Публичный календарь;
 4. Пользователь после открытия приложения оказывается на вкладке личного календаря, здесь он создал несколько событий на ближайшие дни - пока
создавал получил несколько предупреждений о пересечении событий и 2 раза получил ошибку при попытке создания обязательных событий друг на дружке
 6. Пользователь зашёл в личный кабинет поменял себе имя (сохранив, приложение проверило, что такого имени больше не существует), установил нужную палитру цветов;
 7. Пользователь зашел на вкладку публичного календаря, где он создал несколько событий сообщающих о датах и времени когда он с другом собирается поиграть в настолки;
 8. Пользователь подписался на свои же публичные события после чего те отобразились у него в личном календаре.
 9. Увидел несколько предупреждений о пересечении важных событий и отредактировал их, но при этом не смог повлиять на публичные события.
 10. Скинул номер событий друзьям, те на них подписались, в публичных событиях пользователя отобразился список подписавшихся на данное событие.
 11. Пользователь зашёл с другого устройства и увидел своё расписание.

 Компоненты системы:
 1. Система атрибутов
 2. Система хранения событий как личных так и публичных
 3. Система синхронизации данных
 4. Система отображения данных
 5. Система правил и реагирования на них
 6. Система регистрации
 7. Система фильтраций и объединений
 8. Система персонализации

 Стадии разработки проекта:
 1. Реализация всех систем
 2. Реализация бд для хранения данных о пользователях и событиях, настройка взаимодействия приложения с бд, размещение бд
 3. Реализация внешнего вида приложения

 Точки расширения кода:
 - Атрибуты
 - Фильтры на календаре
 - Функции правил проверки
 
 Предполагаются следующие обязательные атрибуты:
 - Дата : DATA
 - Время начала : TIME
 - Время конца : TIME
   
 Предполагаются следующие необязательные атрибуты:
 - Является выполнимым : BOOL
 - Выполнено : BOOL
 - Обязательное : BOOL
 - Зависит от расположения : BOOL
 - Расположение : STRING
 - Единоразовость : BOOL
 - Регулярность : (ENUM, INT)
 - Дата старта : DATA
 - Дата окончания : DATA
