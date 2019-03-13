using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;
using VkNet.Model.RequestParams;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        [Route("test")]
        public IActionResult TestVkMethod([FromBody] TestQuerry query)
        {
            if (query.type == "confirmation" && query.group_id == 179639072 && query.secret == "xuxycpou2349ppsd9mm3207ALSH")
            {
                return Ok("76d2ff7e");
            }
            else if (query.type == "message_new" && query.secret == "xuxycpou2349ppsd9mm3207ALSH")
            {
                var api = new VkApi();
                api.Authorize(new ApiAuthParams
                {
                    Settings = Settings.All,
                    AccessToken = "8cf2c7671bcaf6b04977e92fe87101c0ff647fd134f09984531b95e86488b95e6f9ebbb0b4cc6099f6f60"
                });
                string messageFinal;
                if (query.@object.text == "привет")
                {
                    messageFinal = query.@object.text;
                }
                else if(query.@object.text == "пока")
                {
                    messageFinal = query.@object.text;
                }
                else if (query.@object.text == "Что делаешь")
                {
                    messageFinal = "Я же бот, я просто существую";
                }
                else if (query.@object.text == "Как дела")
                {
                    messageFinal = "Отлично, питание все еще не отключили!";
                }
                else if (query.@object.text == "Ты мне не нравишься")
                {
                    messageFinal = "Было бы жаль это слышать, но нет. Жаль, что я ничего не чувствую, чтобы было жаль";
                }
                else
                {
                    return Ok("ok");
                }

                #region ОТПРАВКА СООБЩЕНИЯ ПОЛЬЗОВАТЕЛЮ
                MessagesGetObject mgo = api.Messages.GetHistory(new MessagesGetHistoryParams
                {
                    UserId = query.@object.from_id,
                    Count = 1
                });

                VkNet.Model.Keyboard.KeyboardBuilder builder = new VkNet.Model.Keyboard.KeyboardBuilder();
                builder.SetOneTime();
                builder.AddButton("Что делаешь", "1", VkNet.Enums.SafetyEnums.KeyboardButtonColor.Positive, "1");
                builder.AddButton("Как дела", "2", VkNet.Enums.SafetyEnums.KeyboardButtonColor.Primary, "2");
                builder.AddButton("Ты мне не нравишься", "3", VkNet.Enums.SafetyEnums.KeyboardButtonColor.Negative, "3");

                if (mgo.Messages.Last().FromId == query.@object.from_id)
                {
                    api.Messages.Send(new MessagesSendParams
                    {
                        UserId = query.@object.from_id,
                        Message = messageFinal,
                        //Keyboard = new VkNet.Model.Keyboard.MessageKeyboard { Buttons = those, OneTime = true },
                        Keyboard = builder.Build(),
                        //Payload = "{\"button\": \"1\"}"
                    });
                }
                return Ok("ok");
                #endregion
            }
            else
            {
                return Ok("jipa");
            }
        }
    }
}
