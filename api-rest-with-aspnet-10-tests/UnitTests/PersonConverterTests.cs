using api_rest_with_aspnet_10.Data.Converter.Implementations;
using api_rest_with_aspnet_10.Data.DTO.V2;
using api_rest_with_aspnet_10.Models;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;

namespace api_rest_with_aspnet_10_tests.UnitTests;

public class PersonConverterTests
{
    private readonly PersonConverter _converter;

    public PersonConverterTests()
    {
        _converter = new PersonConverter();
    }

    //Esse fact diz que esse método é um teste unitário
    //O nome do teste deve ser descritivo, indicando o que está sendo testado e qual é o resultado esperado
    //Com o nome do teste, no log fica mais facil ver onde o teste falhou, sem precisar abrir o código do teste para entender o que ele faz
    //Ex: PersonDTO to Person conversion tests
    [Fact]
    public void Parse_ShouldConvertPersonDTOToPerson()
    {
        //Testes unitários devem seguir a estrutura Arrange, Act e Assert
        //Arrange: Configura o ambiente para o teste
        var dto = new PersonDTO
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            Address = "123 Main",
            Gender = "Male",
            Birthday = new DateTime(1994, 5, 25)
        };

        var expectedPerson = new Person
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            Address = "123 Main",
            Gender = "Male"
        };

        //Act: Executa a ação que queremos testar
        var actualPerson = _converter.Parse(dto);

        //Assert: Verifica se o resultado é o esperado
        actualPerson.Should().NotBeNull();
        actualPerson.Id.Should().Be(expectedPerson.Id);
        actualPerson.FirstName.Should().Be(expectedPerson.FirstName);
        actualPerson.LastName.Should().Be(expectedPerson.LastName);
        actualPerson.Gender.Should().Be(expectedPerson.Gender);
        actualPerson.Should().BeEquivalentTo(expectedPerson);

    }

    //Aqui estou testanto o comportamento do método Parse quando recebe um objeto nulo.
    //O resultado esperado é que o método retorne nulo, indicando que não é possível converter um objeto nulo para um objeto Person.
    [Fact]
    public void Parse_NullPersonDTO_ShouldReturnNull()
    {
        //Arrange
        PersonDTO dto = null;
        //Act
        var actualPerson = _converter.Parse(dto);
        //Assert
        actualPerson.Should().BeNull();
    }

    //aqui vai converter um objeto Person para um objeto PersonDTO, usando o método Parse do PersonConverter.
    //O teste verifica se a conversão foi feita corretamente, comparando as propriedades do objeto resultante com as propriedades do objeto esperado.
    [Fact]
    public void Parse_ShouldConvertPersonToPersonDTO()
    {
        //Testes unitários devem seguir a estrutura Arrange, Act e Assert
        //Arrange: Configura o ambiente para o teste
        var entity = new Person
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            Address = "123 Main",
            Gender = "Male",
            //Birthday = new DateTime(1994, 5, 25)
        };

        var expectedPerson = new PersonDTO
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            Address = "123 Main",
            Gender = "Male"
        };

        //Act: Executa a ação que queremos testar
        var actualPerson = _converter.Parse(entity);

        //Assert: Verifica se o resultado é o esperado
        actualPerson.Should().NotBeNull();
        actualPerson.Id.Should().Be(expectedPerson.Id);
        actualPerson.FirstName.Should().Be(expectedPerson.FirstName);
        actualPerson.LastName.Should().Be(expectedPerson.LastName);
        actualPerson.Gender.Should().Be(expectedPerson.Gender);
        actualPerson.Should().BeEquivalentTo(expectedPerson, options => options.Excluding(expectedPerson => expectedPerson.Birthday));
        actualPerson.Birthday.Should().NotBeNull(); // Validando se a propriedade Birthday não é nula, mesmo que o valor seja null, para garantir que a propriedade foi mapeada corretamente.

    }

    //Aqui estou testanto o comportamento do método Parse quando recebe um objeto nulo.
    //O resultado esperado é que o método retorne nulo, indicando que não é possível converter um objeto nulo para um objeto Person.
    [Fact]
    public void Parse_NullPerson_ShouldReturnNull()
    {
        //Arrange
        Person dto = null;
        //Act
        var actualPerson = _converter.Parse(dto);
        //Assert
        actualPerson.Should().BeNull();
    }

    //Aqui estou testando o método ParseList, que é responsável por converter uma lista de objetos PersonDTO para uma lista de objetos Person.
    [Fact]
    public void ParseList_ShouldConvertListOfPersonDTOToListOfPerson()
    {
        //Arrange
        var dtosList = new List<PersonDTO>
        {
            new PersonDTO
            {
                Id = 1,
                FirstName = "Kelvim",
                LastName = "Rodrigues",
                Address = "Novo Hamburgo",
                Gender = "Male",
                Birthday = new DateTime(1994, 5, 25)
            },
            new PersonDTO
            {
                Id = 2,
                FirstName = "Sophia",
                LastName = "Emanuelly",
                Address = "Novo Hamburgo",
                Gender = "Female",
                Birthday = new DateTime(2015, 11, 26)
            }
        };


        //Act
        var personList = _converter.ParseList(dtosList);

        //Assert
        personList.Should().NotBeNull();
        personList.Should().HaveCount(2);

        personList[0].Should().BeEquivalentTo(new Person
        {
            Id = 1,
            FirstName = "Kelvim",
            LastName = "Rodrigues",
            Address = "Novo Hamburgo",
            Gender = "Male",
            //Birthday = new DateTime(1994, 5, 25)
        });

        personList[1].Should().BeEquivalentTo(new Person
        {
            Id = 2,
            FirstName = "Sophia",
            LastName = "Emanuelly",
            Address = "Novo Hamburgo",
            Gender = "Female",
            //Birthday = new DateTime(2015, 11, 26)
        });

        //Consigo pegar tambem as propriedades individualmente, caso queira validar apenas uma ou outra propriedade, sem precisar validar o objeto inteiro.
        personList[0].FirstName.Should().Be("Kelvim");
        personList[1].FirstName.Should().Be("Sophia");
    }

    //Valida a listagem de pessoas 
    [Fact]
    public void Parse_NullListPersonDTOShouldReturnNull()
    {
        //Arrange
        List<Person> dto = null;
        //Act
        var listPerson = _converter.ParseList(dto);
        //Assert
        listPerson.Should().BeNull();
    }

    //Aqui estou testando o método ParseList, que é responsável por converter uma lista de objetos Person para uma lista de objetos PersonDTO.
    [Fact]
    public void ParseList_ShouldConvertListOfPersonToListOfPersonDTO()
    {
        //Arrange
        var entitiesList = new List<Person>
        {
            new Person
            {
                Id = 1,
                FirstName = "Kelvim",
                LastName = "Rodrigues",
                Address = "Novo Hamburgo",
                Gender = "Male"               
            },
            new Person
            {
                Id = 2,
                FirstName = "Sophia",
                LastName = "Rodrigues",
                Address = "Novo Hamburgo",
                Gender = "Female"
            },
            new Person
            {
                Id = 3,
                FirstName = "Davi",
                LastName = "Lucca",
                Address = "Novo Hamburgo",
                Gender = "Male"
            },
            new Person
            {
                Id = 4,
                FirstName = "Cassio",
                LastName = "Rodrigues",
                Address = "Novo Hamburgo",
                Gender = "Male"
            }
        };

        //Act
        var personList = _converter.ParseList(entitiesList);

        //Assert
        personList.Should().NotBeNull();
        personList.Should().HaveCount(4);

        //Consigo pegar tambem as propriedades individualmente, caso queira validar apenas uma ou outra propriedade, sem precisar validar o objeto inteiro.
        personList[2].FirstName.Should().Be("Davi");
        personList[3].Gender.Should().Be("Male");
    }

    //Aqui verifico o comportamento do método ParseList quando recebe um objeto nulo.
    //O resultado esperado é que o método retorne nulo, indicando que não é possível converter uma lista nula para uma lista de objetos PersonDTO.
    [Fact]
    public void Parse_NullListPersonShouldReturnNull()
    {
        //Arrange
        List<PersonDTO> dto = null;
        //Act
        var listPerson = _converter.ParseList(dto);
        //Assert
        listPerson.Should().BeNull();
    }
}