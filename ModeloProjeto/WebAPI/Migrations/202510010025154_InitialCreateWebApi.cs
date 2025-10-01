namespace WebAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreateWebApi : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cliente",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CPF = c.String(),
                        Nome = c.String(),
                        RG = c.String(),
                        DataExpedicao = c.DateTime(nullable: false),
                        OrgaoExpedicao = c.String(),
                        UF = c.String(),
                        DataNascimento = c.DateTime(nullable: false),
                        Sexo = c.String(),
                        EstadoCivil = c.String(),
                        EnderecoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Endereco", t => t.EnderecoId, cascadeDelete: true)
                .Index(t => t.EnderecoId);
            
            CreateTable(
                "dbo.Endereco",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CEP = c.String(),
                        Logradouro = c.String(),
                        Numero = c.String(),
                        Complemento = c.String(),
                        Bairro = c.String(),
                        Cidade = c.String(),
                        UF = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cliente", "EnderecoId", "dbo.Endereco");
            DropIndex("dbo.Cliente", new[] { "EnderecoId" });
            DropTable("dbo.Endereco");
            DropTable("dbo.Cliente");
        }
    }
}
