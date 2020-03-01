using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HerbMagicWebApi.Common
{
    public class Data
    {
        public static List<BankRequestModels> BankRequestList =
           new List<BankRequestModels>() { new BankRequestModels() {
               Currey ="JPY", Url="https://www.moneydj.com/JsonData/iQuote/iQuoteJsonData.xdjjson?x=AXEX&a=AX000030"}
           ,new BankRequestModels() {
               Currey ="USD", Url="https://www.moneydj.com/JsonData/iQuote/iQuoteJsonData.xdjjson?x=AXEX&a=AX000020"}
           ,new BankRequestModels(){
               Currey ="CNY", Url="https://www.moneydj.com/JsonData/iQuote/iQuoteJsonData.xdjjson?x=AXEX&a=AX000250"}
           ,new BankRequestModels(){
               Currey ="KRW", Url="https://www.moneydj.com/JsonData/iQuote/iQuoteJsonData.xdjjson?x=AXEX&a=AX000300"}
           ,new BankRequestModels(){
               Currey ="AUD", Url="https://www.moneydj.com/JsonData/iQuote/iQuoteJsonData.xdjjson?x=AXEX&a=AX000070"}
           ,new BankRequestModels(){
               Currey ="HKD", Url="https://www.moneydj.com/JsonData/iQuote/iQuoteJsonData.xdjjson?x=AXEX&a=AX000110"}
           ,new BankRequestModels(){
               Currey ="GBP", Url="https://www.moneydj.com/JsonData/iQuote/iQuoteJsonData.xdjjson?x=AXEX&a=AX000040"}
           ,new BankRequestModels(){
               Currey ="NZD", Url="https://www.moneydj.com/JsonData/iQuote/iQuoteJsonData.xdjjson?x=AXEX&a=AX000080"}
           ,new BankRequestModels(){
               Currey ="EUR", Url="https://www.moneydj.com/JsonData/iQuote/iQuoteJsonData.xdjjson?x=AXEX&a=AX000090"}
           ,new BankRequestModels(){
               Currey ="SGD", Url="https://www.moneydj.com/JsonData/iQuote/iQuoteJsonData.xdjjson?x=AXEX&a=AX000140"}
           ,new BankRequestModels(){
               Currey ="THB", Url="https://www.moneydj.com/JsonData/iQuote/iQuoteJsonData.xdjjson?x=AXEX&a=AX000200"}
           ,new BankRequestModels(){
               Currey ="PHP", Url="https://www.moneydj.com/JsonData/iQuote/iQuoteJsonData.xdjjson?x=AXEX&a=AX000210"}
           ,new BankRequestModels(){
               Currey ="MXN", Url="https://www.moneydj.com/JsonData/iQuote/iQuoteJsonData.xdjjson?x=AXEX&a=AX000260"}

           };

    }
}