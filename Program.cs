using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using static System.Net.Mime.MediaTypeNames;

namespace buckshot_beta_
{
    enum gamestart
    {
        게임시작 = 1, 게임종료 = 2
    }
    enum choose
    {
        나 = 1, 상대 = 2
    }


    internal class Program
    {

        static void Main(string[] args)
        {
            string[] anime = new string[2];

            anime[0] = "-#@@%@@@@@@%%%%%#%@@\r\n-#@%###%%##********%@\r\n *@%%@@@@@@#########\r\n *%+##%%=#*         \r\n@*+*#@@##-          \r\n@%#*%@*        ";
            anime[1] = "                                         \r\n                            .-=======-.  \r\n    -********************+. -%#+=+++#%:  \r\n  :#@%%*+#%@@%%*==::=::=%#. -%+:::::*%:  \r\n    =@@%%%%%%%%%%%%%%%%%@#. -%+:::::*%:  \r\n    =@##%%%%@@@*+++++++++=. -%#*****%%:  \r\n    =@**##%@++@- .:-------:.  \r\n  :##=+*#@@@@*                           \r\n  :#%*##%@*                              \r\n  .-==--==-                              \r\n                        ";


            gamestart startEnd;
            choose userChoose;
            int computerBullet;
            int userlife;
            int computerlife;
            int computerchoose;
            int count = 0;


            //게임 시작할껀지 종료할껀지 결정
            #region 게임 시작할껀지 종료할껀지 결정
            while (true)
            {

                Console.WriteLine("게임시작 (1 또는 게임시작을 적어주세요.)");
                Console.WriteLine();
                Console.WriteLine("게임종료 (2 또는 게임종료을 적어주세요.)");
                Console.WriteLine();
                Console.Write("입력칸:  ");
                Enum.TryParse(Console.ReadLine(), out startEnd);
                Console.WriteLine();
                switch (startEnd)
                {
                    case gamestart.게임시작:
                        Console.WriteLine("게임을 시작합니다");
                        Thread.Sleep(1000);
                        Console.Clear();
                        break;
                    case gamestart.게임종료:
                        Console.WriteLine("게임을 종료합니다");
                        Thread.Sleep(1000);
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("제대로 입력해주세요");
                        Thread.Sleep(1000);
                        Console.Clear();
                        continue;
                }
                if (startEnd == gamestart.게임시작)
                {
                    break;
                }
            }
            #endregion

            //총알 설정
            #region 총알 설정

            Random Bullet = new Random();//랜덤총알숫자 만들것
            int realSlug = Bullet.Next(1, 5);//실탄 갯수
            int fakeSlug = Bullet.Next(1, 5);//공포탄갯수
            count = 0;
            int comrealSlug = realSlug;//컴퓨터가 알 실탄갯수 
            int comfakeSlug = fakeSlug;//컴퓨터가 알 공포탄 갯수

            int sumslug = realSlug + fakeSlug;//총알 총합
            int[] Startslug = new int[sumslug];//총알을구별해줄것들

            #region 총알 알려주기
            Console.WriteLine($"실탄 {realSlug}개      공포탄{fakeSlug}개 있습니다");// 실탄 공포탄 갯수 알려주기
            Thread.Sleep(7000);
            Console.Clear();
            #endregion


            for (int i = 0; i < Startslug.Length; i++)//총알을 안에다가 넣는것
            {

                Startslug[i] = Bullet.Next(1, 3);//1이면 실탄 2이면 공포탄
                Thread.Sleep(10);
                if (Startslug[i] == 1 && realSlug > 0)
                {
                    realSlug--;
                }
                else if (Startslug[i] == 2 && fakeSlug > 0)
                {
                    fakeSlug--;
                }
                else if (Startslug[i] == 2 && fakeSlug <= 0 && realSlug > 0)
                {
                    Startslug[i] = 1;
                    realSlug--;
                }
                else if (Startslug[i] == 1 && fakeSlug > 0 && realSlug <= 0)
                {
                    fakeSlug--;
                    Startslug[i] = 2;
                }
                else
                {
                    for (int j = 0; j < Startslug.Length; j++)
                    {
                        Console.WriteLine(Startslug[j]);
                        Console.WriteLine("이상한데");
                    }
                }

            }

            for (int i = 3; i > 0; i--)
            {
                Console.WriteLine($"총알을 집어넣는중입니다. {i + "초"}");
                Thread.Sleep(1000);
                Console.Clear();
            }
            #endregion

            #region 목숨 초기값
            userlife = 4;
            computerlife = 4;
            #endregion

            #region 게임 진행

            while (userlife > 0 && computerlife > 0)
            {
                #region 유저가 총 쏠때

                #region 생존 체크
                if (computerlife == 0)
                {
                    Console.Clear();
                    Console.WriteLine("축하합니다 당신이 이기셨습니다 !!");
                    break;
                }
                else if (userlife == 0)
                {
                    Console.Clear();
                    Console.WriteLine("당신은 사망하셨습니다. ㅠㅠ");
                    break;
                }
                #endregion
                Console.Clear();
                Console.WriteLine("유저의 턴입니다.");
                Console.WriteLine();

                while (true)
                {

                    #region 총알 체크 장전
                    if (count == sumslug)
                    {
                        Console.WriteLine("총알이 없네요! (재장전!)");
                        Console.WriteLine();
                        realSlug = Bullet.Next(1, 5);//실탄 갯수
                        fakeSlug = Bullet.Next(1, 5);//공포탄갯수
                        count = 0;
                        comrealSlug = realSlug;//컴퓨터가 알 실탄갯수 
                        comfakeSlug = fakeSlug;//컴퓨터가 알 공포탄 갯수

                        sumslug = realSlug + fakeSlug;//총알 총합
                        Startslug = new int[sumslug];//총알을구별해줄것들

                        #region 총알 알려주기
                        Console.WriteLine($"실탄 {realSlug}개      공포탄{fakeSlug}개 있습니다");// 실탄 공포탄 갯수 알려주기
                        Thread.Sleep(7000);
                        Console.Clear();
                        #endregion


                        for (int i = 0; i < Startslug.Length; i++)//총알을 안에다가 넣는것
                        {

                            Startslug[i] = Bullet.Next(1, 3);//1이면 실탄 2이면 공포탄
                            Thread.Sleep(10);
                            if (Startslug[i] == 1 && realSlug > 0)
                            {
                                realSlug--;
                            }
                            else if (Startslug[i] == 2 && fakeSlug > 0)
                            {
                                fakeSlug--;
                            }
                            else if (Startslug[i] == 2 && fakeSlug <= 0 && realSlug > 0)
                            {
                                Startslug[i] = 1;
                                realSlug--;
                            }
                            else if (Startslug[i] == 1 && fakeSlug > 0 && realSlug <= 0)
                            {
                                fakeSlug--;
                                Startslug[i] = 2;
                            }
                            else
                            {
                                for (int j = 0; j < Startslug.Length; j++)
                                {
                                    Console.WriteLine(Startslug[j]);
                                    Console.WriteLine("이상한데");
                                    Environment.Exit(0);
                                }
                            }
                        }

                        for (int i = 3; i > 0; i--)
                        {
                            Console.WriteLine($"총알을 집어넣는중입니다. {i + "초"}");
                            Thread.Sleep(1000);
                            Console.Clear();
                        }
                    }
                    #endregion

                    // 치트키!! 1은 실탄 2는 공포탄
                    //for (int j = 0; j < Startslug.Length; j++)
                    //{
                    //    Console.WriteLine(Startslug[j]);
                    //}


                    Console.WriteLine("어떻게 하시겠습니까?");
                    Console.WriteLine();
                    Console.WriteLine("나한테 쏜다 (1,나)     상대를 쏜다(2,상대)");
                    Enum.TryParse(Console.ReadLine(), out userChoose);
                    Console.Clear();
                    switch (userChoose)
                    {
                        case choose.나:
                            if (Startslug[count] == 1)
                            {
                                Console.Clear();
                                Console.WriteLine(anime[0]);
                                Thread.Sleep(1000);
                                Console.Clear();
                                Console.WriteLine(anime[1]);
                                Thread.Sleep(1000);
                                Console.Clear();
                                Console.WriteLine("실탄으로 자해하셨습니다.");
                                count++;
                                userlife--;
                                comrealSlug--;
                                Thread.Sleep(3000);
                                Console.WriteLine();
                                Console.WriteLine($"유저 목숨{userlife}개 남았습니다");
                                Thread.Sleep(3000);
                                break;
                            }
                            else if (Startslug[count] == 2)
                            {
                                Console.Clear();
                                Console.WriteLine(anime[0]);
                                Thread.Sleep(1000);
                                Console.Clear();
                                Console.WriteLine(anime[1]);
                                Thread.Sleep(1000);
                                Console.Clear();
                                Console.WriteLine("나자신에게 공포탄을 쏘셨습니다 (한번 더 쏠 수 있습니다.)");
                                count++;
                                comfakeSlug--;
                                Thread.Sleep(3000);
                                Console.Clear();
                                continue;
                            }
                            else
                            {
                                Console.WriteLine("나한테 총쏠때 오류 if문");
                            }
                            break;

                        case choose.상대:
                            if (Startslug[count] == 1)
                            {
                                Console.Clear();
                                Console.WriteLine(anime[0]);
                                Thread.Sleep(1000);
                                Console.Clear();
                                Console.WriteLine(anime[1]);
                                Thread.Sleep(1000);
                                Console.Clear();
                                Console.WriteLine("컴퓨터는 총에 맞았습니다");
                                count++;
                                computerlife--;
                                comrealSlug--;
                                Thread.Sleep(3000);
                                Console.WriteLine();
                                Console.WriteLine($"상대 목숨은 {computerlife}개 남았습니다.");
                                Thread.Sleep(3000);
                                break;
                            }
                            else if (Startslug[count] == 2)
                            {
                                Console.Clear();
                                Console.WriteLine(anime[0]);
                                Thread.Sleep(1000);
                                Console.Clear();
                                Console.WriteLine(anime[1]);
                                Thread.Sleep(1000);
                                Console.Clear();
                                Console.WriteLine("컴퓨터한테 공포탄을 쏘셨네요.");
                                comfakeSlug--;
                                count++;
                                Thread.Sleep(3000);
                                break;
                            }
                            else
                            {
                                Console.WriteLine("상대한테 총쏠때 오류 if문");
                            }
                            break;

                        default:
                            {
                                Console.WriteLine("제대로 입력해주세요");
                                Thread.Sleep(1000);
                                Console.Clear();
                                continue;
                            }
                    }
                    break;
                }
                #endregion



                #region 총알 체크 장전
                if (count == sumslug)
                {
                    Console.Clear();
                    Console.WriteLine("총알이 없네요! (재장전!)");
                    Console.WriteLine();
                    realSlug = Bullet.Next(1, 5);//실탄 갯수
                    fakeSlug = Bullet.Next(1, 5);//공포탄갯수
                    count = 0;
                    comrealSlug = realSlug;//컴퓨터가 알 실탄갯수 
                    comfakeSlug = fakeSlug;//컴퓨터가 알 공포탄 갯수

                    sumslug = realSlug + fakeSlug;//총알 총합
                    Startslug = new int[sumslug];//총알을구별해줄것들

                    #region 총알 알려주기
                    Console.WriteLine($"실탄 {realSlug}개      공포탄{fakeSlug}개 있습니다");// 실탄 공포탄 갯수 알려주기
                    Thread.Sleep(7000);
                    Console.Clear();
                    #endregion


                    for (int i = 0; i < Startslug.Length; i++)//총알을 안에다가 넣는것
                    {

                        Startslug[i] = Bullet.Next(1, 3);//1이면 실탄 2이면 공포탄
                        Thread.Sleep(10);
                        if (Startslug[i] == 1 && realSlug > 0)
                        {
                            realSlug--;
                        }
                        else if (Startslug[i] == 2 && fakeSlug > 0)
                        {
                            fakeSlug--;
                        }
                        else if (Startslug[i] == 2 && fakeSlug <= 0 && realSlug > 0)
                        {
                            Startslug[i] = 1;
                            realSlug--;
                        }
                        else if (Startslug[i] == 1 && fakeSlug > 0 && realSlug <= 0)
                        {
                            fakeSlug--;
                            Startslug[i] = 2;
                        }
                        else
                        {
                            for (int j = 0; j < Startslug.Length; j++)
                            {
                                Console.WriteLine(Startslug[j]);
                                Console.WriteLine("이상한데");
                                Environment.Exit(0);
                            }
                        }
                    }

                    for (int i = 3; i > 0; i--)
                    {
                        Console.WriteLine($"총알을 집어넣는중입니다. {i + "초"}");
                        Thread.Sleep(1000);
                        Console.Clear();
                    }
                }
                #endregion



                #region 컴퓨터 행동
                while (true)
                {
                    #region 생존 체크
                    if (computerlife == 0)
                    {
                        Console.Clear();
                        Console.WriteLine("축하합니다 당신이 이기셨습니다 !!");
                        break;
                    }
                    else if (userlife == 0)
                    {
                        Console.Clear();
                        Console.WriteLine("당신은 사망하셨습니다. ㅠㅠ");
                        break;
                    }
                    #endregion

                    #region 총알 체크 장전
                    if (count == sumslug)
                    {
                        Console.Clear();
                        Console.WriteLine("총알이 없네요! (재장전!)");
                        Console.WriteLine();

                        realSlug = Bullet.Next(1, 5);//실탄 갯수
                        fakeSlug = Bullet.Next(1, 5);//공포탄갯수
                        count = 0;
                        comrealSlug = realSlug;//컴퓨터가 알 실탄갯수 
                        comfakeSlug = fakeSlug;//컴퓨터가 알 공포탄 갯수

                        sumslug = realSlug + fakeSlug;//총알 총합
                        Startslug = new int[sumslug];//총알을구별해줄것들

                        #region 총알 알려주기
                        Console.WriteLine($"실탄 {realSlug}개      공포탄{fakeSlug}개 있습니다");// 실탄 공포탄 갯수 알려주기
                        Thread.Sleep(7000);
                        Console.Clear();
                        #endregion


                        for (int i = 0; i < Startslug.Length; i++)//총알을 안에다가 넣는것
                        {
                            Startslug[i] = Bullet.Next(1, 3);//1이면 실탄 2이면 공포탄
                            Thread.Sleep(10);
                            if (Startslug[i] == 1 && realSlug > 0)
                            {
                                realSlug--;
                            }
                            else if (Startslug[i] == 2 && fakeSlug > 0)
                            {
                                fakeSlug--;
                            }
                            else if (Startslug[i] == 2 && fakeSlug <= 0 && realSlug > 0)
                            {
                                Startslug[i] = 1;
                                realSlug--;
                            }
                            else if (Startslug[i] == 1 && fakeSlug > 0 && realSlug <= 0)
                            {
                                fakeSlug--;
                                Startslug[i] = 2;
                            }
                            else
                            {
                                for (int j = 0; j < Startslug.Length; j++)
                                {
                                    Console.WriteLine(Startslug[j]);
                                    Console.WriteLine("이상한데");
                                    Environment.Exit(0);
                                }
                            }
                        }

                        for (int i = 3; i > 0; i--)
                        {
                            Console.WriteLine($"총알을 집어넣는중입니다. {i + "초"}");
                            Thread.Sleep(1000);
                            Console.Clear();
                        }
                    }
                    #endregion
                    Console.Clear();
                    Console.WriteLine("컴퓨터 차례입니다.");
                    Thread.Sleep(3000);
                    Console.Clear();
                    computerBullet = comrealSlug + comfakeSlug;
                    computerchoose = Bullet.Next(1, computerBullet + 1);
                    Console.Clear();
                    if (computerchoose <= comrealSlug)//컴퓨터 유저에게 사격
                    {
                        Console.WriteLine("컴퓨터 유저사격!!");
                        Console.WriteLine();
                        Thread.Sleep(3000);
                        if (Startslug[count] == 1)
                        {
                            Console.Clear();
                            Console.WriteLine(anime[0]);
                            Thread.Sleep(1000);
                            Console.Clear();
                            Console.WriteLine(anime[1]);
                            Thread.Sleep(1000);
                            Console.Clear();
                            Console.WriteLine("컴퓨터한테 실탄을 맞으셨습니다");
                            Console.WriteLine();
                            Thread.Sleep(3000);
                            count++;
                            userlife--;
                            comrealSlug--;
                            Console.WriteLine($"유저목숨 {userlife}개 남았습니다.");
                            Thread.Sleep(3000);
                            break;
                        }
                        else if (Startslug[count] == 2)
                        {
                            Console.Clear();
                            Console.WriteLine(anime[0]);
                            Thread.Sleep(1000);
                            Console.Clear();
                            Console.WriteLine(anime[1]);
                            Thread.Sleep(1000);
                            Console.Clear();
                            Console.WriteLine("공포탄입니다.(컴퓨터 유저사격 실패)");
                            count++;
                            comfakeSlug--;
                            Thread.Sleep(3000);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("컴퓨터 유저사격 if 문");

                        }

                    }
                    else//자신에게 사격
                    {
                        Console.WriteLine("컴퓨터 자기 자신에게 사격!!");
                        Console.WriteLine();
                        Thread.Sleep(3000);
                        if (Startslug[count] == 1)
                        {
                            Console.Clear();
                            Console.WriteLine(anime[0]);
                            Thread.Sleep(1000);
                            Console.Clear();
                            Console.WriteLine(anime[1]);
                            Thread.Sleep(1000);
                            Console.Clear();
                            Console.WriteLine("컴퓨터는 자해를 하고야 말았다!!");
                            Thread.Sleep(3000);
                            count++;
                            computerlife--;
                            comrealSlug--;
                            break;
                        }
                        else if (Startslug[count] == 2)
                        {
                            Console.Clear();
                            Console.WriteLine(anime[0]);
                            Thread.Sleep(1000);
                            Console.Clear();
                            Console.WriteLine(anime[1]);
                            Thread.Sleep(1000);
                            Console.Clear();
                            Console.WriteLine("컴퓨터는 자기 자신에게 공포탄을 쌌다");
                            Thread.Sleep(3000);
                            count++;
                            comfakeSlug--;
                            continue;
                        }
                        else
                        {
                            Console.WriteLine("컴퓨터 자기 자신 if 문");
                        }
                    }

                }

                #region 생존 체크
                if (computerlife == 0)
                {
                    Console.Clear();
                    Console.WriteLine("축하합니다 당신이 이기셨습니다 !!");
                    break;
                }
                else if (userlife == 0)
                {
                    Console.Clear();
                    Console.WriteLine("당신은 사망하셨습니다. ㅠㅠ");
                    break;
                }
                #endregion

                #endregion
            }
            #endregion


        }
    }
}
