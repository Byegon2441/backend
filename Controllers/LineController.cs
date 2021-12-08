using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Interface;
using Microsoft.AspNetCore.Mvc;
using backend.Models;
using System;
using Line.Messaging;
using System.Configuration;
using System.Net.Http;
using Line.Messaging.Webhooks;
using System.Net;
using backend;
using System.Text.Json;
using System.IO;
using System.Text;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LineController : ControllerBase
    {
        private static LineMessagingClient lineMessagingClient;
        private string accessToken = "f+mVVg2lt4mK02vt9T9XWMCmUl7PwgMMC2qizHXD3I49T9xcJt1U+KNCQKlIU193PBlB2mm/97xpufBaLQ8XL0WpEEQRFltHus4geVJtMVIudHvOwWoDiVfDxlvbjguWfyiVFDG707X45SfDG8+33AdB04t89/1O/w1cDnyilFU=";
        private string channelSecret = "530c6737bf58780894af6dfb8717ad8c";
        private readonly IBankService _bankService;
        private readonly IStudentService _studentService;
        string replyMessage = "";
        ISendMessage message;
        List<ITemplateAction> actions = new List<ITemplateAction>();
        List<ITemplateAction> actForQuickReply = new List<ITemplateAction>();
        CarouselColumn carouselColumn = null;


        public LineController(IBankService bankService, IStudentService studentService)
        {
            lineMessagingClient = new LineMessagingClient(accessToken);
            _bankService = bankService;
            _studentService = studentService;

        }


        [HttpPost]
        public async Task<HttpResponseMessage> Post([FromBody] DataFromUser req)
        {

            Console.WriteLine(req.events[0].message.type);
            Console.WriteLine(req.events[0].message.text);

            var userProfile = await lineMessagingClient.GetUserProfileAsync(req.events[0].source.userId);
            Console.WriteLine(userProfile.UserId);
            Console.WriteLine(userProfile.DisplayName);

            // replyUser(req, userProfile);

            Student student = _studentService.GetStudentByID(userProfile.UserId);
            if (req.events[0].message.type == "text")
            {
                switch (req.events[0].message.text)
                {
                    case "ตารางเรียน":
                        replyMessage = student.study;
                        carouselColumn = new CarouselColumn(replyMessage, "https://cdn-icons-png.flaticon.com/512/3652/3652191.png", "ตารางเรียน", actions); break;
                    case "ตารางสอบ":
                        replyMessage = student.test;
                        carouselColumn = new CarouselColumn(replyMessage, "https://cdn-icons.flaticon.com/png/512/297/premium/297443.png?token=exp=1638943905~hmac=c272b3fd243dfddec4335c41dfbcc86f", "ตารางสอบ", actions); break;
                    case "ผลการเรียน":
                        replyMessage = student.grade;
                        actions.Add(new MessageTemplateAction("ดูตามภาคการศึกษา", "ดูตามภาคการศึกษา"));
                        carouselColumn = new CarouselColumn(replyMessage, "https://cdn-icons-png.flaticon.com/512/3874/3874200.png", "ผลการเรียนทั้งหมด", actions);
                        break;
                    case "ค่าใช้จ่าย":
                        replyMessage = student.finance;
                        carouselColumn = new CarouselColumn(replyMessage, "https://cdn-icons.flaticon.com/png/512/2382/premium/2382625.png?token=exp=1638944066~hmac=8e993c36a6abbf59e0736568ebad4f97", "ค่าใช้จ่าย", actions); break;
                    case "Connect":
                        replyMessage = "เชื่อมต่อ Line :" + userProfile.DisplayName + "กับระบบแล้ว";
                        carouselColumn = new CarouselColumn(replyMessage, "https://cdn-icons.flaticon.com/png/512/2881/premium/2881031.png?token=exp=1638944112~hmac=9a1bfeff2b237979ca90d078dfdc6a9a", "เชื่อมต่อ", actions); break;
                    case "ธนาคาร": getBank(); break;
                    case "ดูตามภาคการศึกษา":
                        UriTemplateAction action = new UriTemplateAction("Uri Label", "https://github.com/kenakamu");

                        message = new TemplateMessage("ImageCarouselTemplate",
                            new ImageCarouselTemplate(new List<ImageCarouselColumn>
                            {
                        new ImageCarouselColumn("https://github.com/apple-touch-icon.png", action),
                        new ImageCarouselColumn("https://github.com/apple-touch-icon.png", action),
                        new ImageCarouselColumn("https://github.com/apple-touch-icon.png", action),
                        new ImageCarouselColumn("https://github.com/apple-touch-icon.png", action),
                        new ImageCarouselColumn("https://github.com/apple-touch-icon.png", action),
                        new ImageCarouselColumn("https://github.com/apple-touch-icon.png", action),
                        new ImageCarouselColumn("https://github.com/apple-touch-icon.png", action),
                        new ImageCarouselColumn("https://github.com/apple-touch-icon.png", action)
                            }));
                        break;
                    default:
                        replyMessage = "ไม่เข้าใจคำสั่ง";
                        carouselColumn = new CarouselColumn(replyMessage, "https://cdn-icons.flaticon.com/png/512/3524/premium/3524032.png?token=exp=1638944365~hmac=5351c4e066533c062c7f0eadd13cd170", "ไม่เข้าใจคำสั่ง", actions); break;
                }
                // await lineMessagingClient.ReplyMessageAsync(req.events[0].replyToken, replyMessage);

                //quickreply

                // actions.Add(new PostbackTemplateAction("Postback Label", "sample data", "sample data"));
                actions.Add(new UriTemplateAction("ข้อมูลเพิ่มเติม", "https://liff.line.me/1656702962-Axk9EVE9"));

                actForQuickReply.Add(new MessageTemplateAction("เมนู 1", "เมนู 1"));
                actForQuickReply.Add(new MessageTemplateAction("เมนู 2", "เมนู 2"));
                actForQuickReply.Add(new MessageTemplateAction("เมนู 3", "เมนู 3"));
                actForQuickReply.Add(new MessageTemplateAction("เมนู 4", "เมนู 4"));

                List<QuickReplyButtonObject> quickReplyButtonObject = new List<QuickReplyButtonObject>();
                quickReplyButtonObject.Add(new QuickReplyButtonObject(actForQuickReply[0]));
                quickReplyButtonObject.Add(new QuickReplyButtonObject(actForQuickReply[1]));
                quickReplyButtonObject.Add(new QuickReplyButtonObject(actForQuickReply[2]));
                quickReplyButtonObject.Add(new QuickReplyButtonObject(actForQuickReply[3]));
                QuickReply quickReply = new QuickReply(quickReplyButtonObject);


                if (carouselColumn != null)
                {
                    message = new TemplateMessage("Button Template",
                                       new CarouselTemplate(new List<CarouselColumn>
                                       { carouselColumn  }), quickReply);

                }



                //text
                // message = new TextMessage(replyMessage);

                //picture
                // var imageUrl = "https://th.bing.com/th/id/OIP.v3xevce88aGARzQ6DkzPjgHaEK?w=297&h=180&c=7&r=0&o=5&pid=1.7";
                // message = new ImageMessage(imageUrl, imageUrl);

                //sticker
                // message = new StickerMessage("446", "1998");

                await lineMessagingClient.ReplyMessageAsync(req.events[0].replyToken, new List<ISendMessage> { message });

            }

            HttpResponseMessage res = new HttpResponseMessage(HttpStatusCode.OK);
            return res;
        }

        // public async void replyUser(DataFromUser data, dynamic userProfile)
        // {
        //     Student student = _studentService.GetStudentByID(userProfile.UserId);
        //     switch (data.events[0].message.text)
        //     {
        //         case "ตารางเรียน": replyMessage = student.study; break;
        //         case "ตารางสอบ": replyMessage = student.test; break;
        //         case "ผลการเรียน": replyMessage = student.grade; break;
        //         case "ค่าใช้จ่าย": replyMessage = student.finance; break;
        //         case "Connect": replyMessage = student.lineUserId + "/n" + userProfile.DisplayName; break;
        //         case "ธนาคาร": getBank(); break;
        //         default: replyMessage = "ไม่เข้าใจคำสั่ง"; break;
        //     }
        //     await lineMessagingClient.ReplyMessageAsync(data.events[0].replyToken, replyMessage);
        // }
        public async void getBank()
        {
            IEnumerable<Bank> bank = _bankService.GetAllBank();
            replyMessage = bank.ToString();
        }



    }
}