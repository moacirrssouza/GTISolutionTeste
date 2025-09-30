using System.Runtime.Serialization;

namespace WCFServiceHost.Enums
{
    [DataContract]
    public enum EstadoCivil
    {
        [EnumMember] Solteiro,
        [EnumMember] Casado,
        [EnumMember] Divorciado,
        [EnumMember] Viuvo,
        [EnumMember] Separado,
        [EnumMember] UniaoEstavel
    }
}