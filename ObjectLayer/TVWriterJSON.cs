using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Json;

namespace TVWriter
{
    public class Value2
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string id { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
    }

    public class AwardAwardNominationAward
    {
        public string valuetype { get; set; }
        public List<Value2> values { get; set; }
        public double count { get; set; }
    }

    public class Value3
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string id { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
    }

    public class AwardAwardNominationNominatedFor
    {
        public string valuetype { get; set; }
        public List<Value3> values { get; set; }
        public double count { get; set; }
    }

    public class Value4
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string value { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
    }

    public class AwardAwardNominationYear
    {
        public string valuetype { get; set; }
        public List<Value4> values { get; set; }
        public double count { get; set; }
    }

    public class Value5
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string id { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
    }

    public class TypeObjectAttribution
    {
        public string valuetype { get; set; }
        public List<Value5> values { get; set; }
        public double count { get; set; }
    }

    public class Value6
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string id { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
    }

    public class TypeObjectType
    {
        public string valuetype { get; set; }
        public List<Value6> values { get; set; }
        public double count { get; set; }
    }

    public class Property2
    {
        public AwardAwardNominationAward _award_award_nomination_award { get; set; }
        public AwardAwardNominationNominatedFor _award_award_nomination_nominated_for { get; set; }
        public AwardAwardNominationYear _award_award_nomination_year { get; set; }
        public TypeObjectAttribution _type_object_attribution { get; set; }
        public TypeObjectType _type_object_type { get; set; }
    }

    public class Value
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string id { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
        public Property2 property { get; set; }
    }

    public class AwardAwardNomineeAwardNominations
    {
        public string valuetype { get; set; }
        public List<Value> values { get; set; }
        public double count { get; set; }
    }

    public class Value8
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string id { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
    }

    public class AwardAwardHonorAward
    {
        public string valuetype { get; set; }
        public List<Value8> values { get; set; }
        public double count { get; set; }
    }

    public class Value9
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string id { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
    }

    public class AwardAwardHonorHonoredFor
    {
        public string valuetype { get; set; }
        public List<Value9> values { get; set; }
        public double count { get; set; }
    }

    public class Value10
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string value { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
    }

    public class AwardAwardHonorYear
    {
        public string valuetype { get; set; }
        public List<Value10> values { get; set; }
        public double count { get; set; }
    }

    public class Value11
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string id { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
    }

    public class TypeObjectAttribution2
    {
        public string valuetype { get; set; }
        public List<Value11> values { get; set; }
        public double count { get; set; }
    }

    public class Value12
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string id { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
    }

    public class TypeObjectType2
    {
        public string valuetype { get; set; }
        public List<Value12> values { get; set; }
        public double count { get; set; }
    }

    public class Property3
    {
        public AwardAwardHonorAward _award_award_honor_award { get; set; }
        public AwardAwardHonorHonoredFor _award_award_honor_honored_for { get; set; }
        public AwardAwardHonorYear _award_award_honor_year { get; set; }
        public TypeObjectAttribution2 _type_object_attribution { get; set; }
        public TypeObjectType2 _type_object_type { get; set; }
    }

    public class Value7
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string id { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
        public Property3 property { get; set; }
    }

    public class AwardAwardWinnerAwardsWon
    {
        public string valuetype { get; set; }
        public List<Value7> values { get; set; }
        public double count { get; set; }
    }

    public class Value13
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string id { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
    }

    public class BookAuthorWorksWritten
    {
        public string valuetype { get; set; }
        public List<Value13> values { get; set; }
        public double count { get; set; }
    }

    public class Value14
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string value { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
    }

    public class CommonTopicAlias
    {
        public string valuetype { get; set; }
        public List<Value14> values { get; set; }
        public double count { get; set; }
    }

    public class Value16
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string value { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
    }

    public class CommonDocumentSourceUri
    {
        public string valuetype { get; set; }
        public List<Value16> values { get; set; }
        public double count { get; set; }
    }

    public class Value17
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string value { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
    }

    public class CommonDocumentText
    {
        public string valuetype { get; set; }
        public List<Value17> values { get; set; }
        public double count { get; set; }
    }

    public class Value18
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string id { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
    }

    public class TypeObjectAttribution3
    {
        public string valuetype { get; set; }
        public List<Value18> values { get; set; }
        public double count { get; set; }
    }

    public class Value19
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string id { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
    }

    public class TypeObjectType3
    {
        public string valuetype { get; set; }
        public List<Value19> values { get; set; }
        public double count { get; set; }
    }

    public class Property4
    {
        public CommonDocumentSourceUri _common_document_source_uri { get; set; }
        public CommonDocumentText _common_document_text { get; set; }
        public TypeObjectAttribution3 _type_object_attribution { get; set; }
        public TypeObjectType3 _type_object_type { get; set; }
    }

    public class Value15
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string id { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
        public Property4 property { get; set; }
    }

    public class CommonTopicArticle
    {
        public string valuetype { get; set; }
        public List<Value15> values { get; set; }
        public double count { get; set; }
    }

    public class Value20
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string id { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
    }

    public class CommonTopicImage
    {
        public string valuetype { get; set; }
        public List<Value20> values { get; set; }
        public double count { get; set; }
    }

    public class Value21
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string id { get; set; }
    }

    public class CommonTopicNotableFor
    {
        public string valuetype { get; set; }
        public List<Value21> values { get; set; }
        public double count { get; set; }
    }

    public class Value22
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string id { get; set; }
        public string timestamp { get; set; }
    }

    public class CommonTopicNotableTypes
    {
        public string valuetype { get; set; }
        public List<Value22> values { get; set; }
        public double count { get; set; }
    }

    public class Value23
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string value { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
    }

    public class CommonTopicOfficialWebsite
    {
        public string valuetype { get; set; }
        public List<Value23> values { get; set; }
        public double count { get; set; }
    }

    public class Value24
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string value { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
    }

    public class CommonTopicTopicEquivalentWebpage
    {
        public string valuetype { get; set; }
        public List<Value24> values { get; set; }
        public double count { get; set; }
    }

    public class Value26
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string id { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
    }

    public class FilmPersonalFilmAppearanceFilm
    {
        public string valuetype { get; set; }
        public List<Value26> values { get; set; }
        public double count { get; set; }
    }

    public class Value27
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string id { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
    }

    public class FilmPersonalFilmAppearanceTypeOfAppearance
    {
        public string valuetype { get; set; }
        public List<Value27> values { get; set; }
        public double count { get; set; }
    }

    public class Value28
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string id { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
    }

    public class TypeObjectAttribution4
    {
        public string valuetype { get; set; }
        public List<Value28> values { get; set; }
        public double count { get; set; }
    }

    public class Value29
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string id { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
    }

    public class TypeObjectType4
    {
        public string valuetype { get; set; }
        public List<Value29> values { get; set; }
        public double count { get; set; }
    }

    public class Property5
    {
        public FilmPersonalFilmAppearanceFilm _film_personal_film_appearance_film { get; set; }
        public FilmPersonalFilmAppearanceTypeOfAppearance _film_personal_film_appearance_type_of_appearance { get; set; }
        public TypeObjectAttribution4 _type_object_attribution { get; set; }
        public TypeObjectType4 _type_object_type { get; set; }
    }

    public class Value25
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string id { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
        public Property5 property { get; set; }
    }

    public class FilmPersonOrEntityAppearingInFilmFilms
    {
        public string valuetype { get; set; }
        public List<Value25> values { get; set; }
        public double count { get; set; }
    }

    public class Value30
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string value { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
    }

    public class PeoplePersonDateOfBirth
    {
        public string valuetype { get; set; }
        public List<Value30> values { get; set; }
        public double count { get; set; }
    }

    public class Value32
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string id { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
    }

    public class EducationEducationInstitution
    {
        public string valuetype { get; set; }
        public List<Value32> values { get; set; }
        public double count { get; set; }
    }

    public class Value33
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string id { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
    }

    public class TypeObjectAttribution5
    {
        public string valuetype { get; set; }
        public List<Value33> values { get; set; }
        public double count { get; set; }
    }

    public class Value34
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string id { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
    }

    public class TypeObjectType5
    {
        public string valuetype { get; set; }
        public List<Value34> values { get; set; }
        public double count { get; set; }
    }

    public class Property6
    {
        public EducationEducationInstitution _education_education_institution { get; set; }
        public TypeObjectAttribution5 _type_object_attribution { get; set; }
        public TypeObjectType5 _type_object_type { get; set; }
    }

    public class Value31
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string id { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
        public Property6 property { get; set; }
    }

    public class PeoplePersonEducation
    {
        public string valuetype { get; set; }
        public List<Value31> values { get; set; }
        public double count { get; set; }
    }

    public class Value35
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string id { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
    }

    public class PeoplePersonGender
    {
        public string valuetype { get; set; }
        public List<Value35> values { get; set; }
        public double count { get; set; }
    }

    public class Value36
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string id { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
    }

    public class PeoplePersonNationality
    {
        public string valuetype { get; set; }
        public List<Value36> values { get; set; }
        public double count { get; set; }
    }

    public class Value37
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string id { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
    }

    public class PeoplePersonPlaceOfBirth
    {
        public string valuetype { get; set; }
        public List<Value37> values { get; set; }
        public double count { get; set; }
    }

    public class Value39
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string id { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
    }

    public class PeoplePlaceLivedLocation
    {
        public string valuetype { get; set; }
        public List<Value39> values { get; set; }
        public double count { get; set; }
    }

    public class Value40
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string id { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
    }

    public class TypeObjectAttribution6
    {
        public string valuetype { get; set; }
        public List<Value40> values { get; set; }
        public double count { get; set; }
    }

    public class Value41
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string id { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
    }

    public class TypeObjectType6
    {
        public string valuetype { get; set; }
        public List<Value41> values { get; set; }
        public double count { get; set; }
    }

    public class Property7
    {
        public PeoplePlaceLivedLocation _people_place_lived_location { get; set; }
        public TypeObjectAttribution6 _type_object_attribution { get; set; }
        public TypeObjectType6 _type_object_type { get; set; }
    }

    public class Value38
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string id { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
        public Property7 property { get; set; }
    }

    public class PeoplePersonPlacesLived
    {
        public string valuetype { get; set; }
        public List<Value38> values { get; set; }
        public double count { get; set; }
    }

    public class Value42
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string id { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
    }

    public class PeoplePersonProfession
    {
        public string valuetype { get; set; }
        public List<Value42> values { get; set; }
        public double count { get; set; }
    }

    public class Value44
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string id { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
    }

    public class TvTvProducerTermProgram
    {
        public string valuetype { get; set; }
        public List<Value44> values { get; set; }
        public double count { get; set; }
    }

    public class Value45
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string id { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
    }

    public class TypeObjectAttribution7
    {
        public string valuetype { get; set; }
        public List<Value45> values { get; set; }
        public double count { get; set; }
    }

    public class Value46
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string id { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
    }

    public class TypeObjectType7
    {
        public string valuetype { get; set; }
        public List<Value46> values { get; set; }
        public double count { get; set; }
    }

    public class Property8
    {
        public TvTvProducerTermProgram _tv_tv_producer_term_program { get; set; }
        public TypeObjectAttribution7 _type_object_attribution { get; set; }
        public TypeObjectType7 _type_object_type { get; set; }
    }

    public class Value43
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string id { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
        public Property8 property { get; set; }
    }

    public class TvTvProducerProgramsProduced
    {
        public string valuetype { get; set; }
        public List<Value43> values { get; set; }
        public double count { get; set; }
    }

    public class Value47
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string id { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
    }

    public class TvTvProgramCreatorProgramsCreated
    {
        public string valuetype { get; set; }
        public List<Value47> values { get; set; }
        public double count { get; set; }
    }

    public class Value48
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string id { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
    }

    public class TvTvWriterEpisodesWritten
    {
        public string valuetype { get; set; }
        public List<Value48> values { get; set; }
        public double count { get; set; }
    }

    public class Value50
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string id { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
    }

    public class TvTvProgramWriterRelationshipTvProgram
    {
        public string valuetype { get; set; }
        public List<Value50> values { get; set; }
        public double count { get; set; }
    }

    public class Value51
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string id { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
    }

    public class TypeObjectAttribution8
    {
        public string valuetype { get; set; }
        public List<Value51> values { get; set; }
        public double count { get; set; }
    }

    public class Value52
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string id { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
    }

    public class TypeObjectType8
    {
        public string valuetype { get; set; }
        public List<Value52> values { get; set; }
        public double count { get; set; }
    }

    public class Property9
    {
        public TvTvProgramWriterRelationshipTvProgram _tv_tv_program_writer_relationship_tv_program { get; set; }
        public TypeObjectAttribution8 _type_object_attribution { get; set; }
        public TypeObjectType8 _type_object_type { get; set; }
    }

    public class Value49
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string id { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
        public Property9 property { get; set; }
    }

    public class TvTvWriterTvPrograms
    {
        public string valuetype { get; set; }
        public List<Value49> values { get; set; }
        public double count { get; set; }
    }

    public class Value53
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string id { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
    }

    public class TypeObjectAttribution9
    {
        public string valuetype { get; set; }
        public List<Value53> values { get; set; }
        public double count { get; set; }
    }

    public class Value54
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string value { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
    }

    public class TypeObjectKey
    {
        public string valuetype { get; set; }
        public List<Value54> values { get; set; }
        public double count { get; set; }
    }

    public class Value55
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string value { get; set; }
    }

    public class TypeObjectMid
    {
        public string valuetype { get; set; }
        public List<Value55> values { get; set; }
        public double count { get; set; }
    }

    public class Value56
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string value { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
    }

    public class TypeObjectName
    {
        public string valuetype { get; set; }
        public List<Value56> values { get; set; }
        public double count { get; set; }
    }

    public class Value57
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string id { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
    }

    public class TypeObjectType9
    {
        public string valuetype { get; set; }
        public List<Value57> values { get; set; }
        public double count { get; set; }
    }

    public class Value58
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string id { get; set; }
    }

    public class CommonTopicNotableProperties
    {
        public string valuetype { get; set; }
        public List<Value58> values { get; set; }
        public double count { get; set; }
    }

    public class Value59
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string value { get; set; }
    }

    public class TypeObjectGuid
    {
        public string valuetype { get; set; }
        public List<Value59> values { get; set; }
        public double count { get; set; }
    }

    public class Value60
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string id { get; set; }
        public string timestamp { get; set; }
    }

    public class TypeObjectCreator
    {
        public string valuetype { get; set; }
        public List<Value60> values { get; set; }
        public double count { get; set; }
    }

    public class Value61
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string value { get; set; }
    }

    public class TypeObjectTimestamp
    {
        public string valuetype { get; set; }
        public List<Value61> values { get; set; }
        public double count { get; set; }
    }

    public class Value62
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string id { get; set; }
    }

    public class TypeObjectPermission
    {
        public string valuetype { get; set; }
        public List<Value62> values { get; set; }
        public double count { get; set; }
    }

    public class Property
    {
        public AwardAwardNomineeAwardNominations _award_award_nominee_award_nominations { get; set; }
        public AwardAwardWinnerAwardsWon _award_award_winner_awards_won { get; set; }
        public BookAuthorWorksWritten _book_author_works_written { get; set; }
        public CommonTopicAlias _common_topic_alias { get; set; }
        public CommonTopicArticle _common_topic_article { get; set; }
        public CommonTopicImage _common_topic_image { get; set; }
        public CommonTopicNotableFor _common_topic_notable_for { get; set; }
        public CommonTopicNotableTypes _common_topic_notable_types { get; set; }
        public CommonTopicOfficialWebsite _common_topic_official_website { get; set; }
        public CommonTopicTopicEquivalentWebpage _common_topic_topic_equivalent_webpage { get; set; }
        public FilmPersonOrEntityAppearingInFilmFilms _film_person_or_entity_appearing_in_film_films { get; set; }
        public PeoplePersonDateOfBirth _people_person_date_of_birth { get; set; }
        public PeoplePersonEducation _people_person_education { get; set; }
        public PeoplePersonGender _people_person_gender { get; set; }
        public PeoplePersonNationality _people_person_nationality { get; set; }
        public PeoplePersonPlaceOfBirth _people_person_place_of_birth { get; set; }
        public PeoplePersonPlacesLived _people_person_places_lived { get; set; }
        public PeoplePersonProfession _people_person_profession { get; set; }
        public TvTvProducerProgramsProduced _tv_tv_producer_programs_produced { get; set; }
        public TvTvProgramCreatorProgramsCreated _tv_tv_program_creator_programs_created { get; set; }
        public TvTvWriterEpisodesWritten _tv_tv_writer_episodes_written { get; set; }
        public TvTvWriterTvPrograms _tv_tv_writer_tv_programs { get; set; }
        public TypeObjectAttribution9 _type_object_attribution { get; set; }
        public TypeObjectKey _type_object_key { get; set; }
        public TypeObjectMid _type_object_mid { get; set; }
        public TypeObjectName _type_object_name { get; set; }
        public TypeObjectType9 _type_object_type { get; set; }
        public CommonTopicNotableProperties _common_topic_notable_properties { get; set; }
        public TypeObjectGuid _type_object_guid { get; set; }
        public TypeObjectCreator _type_object_creator { get; set; }
        public TypeObjectTimestamp _type_object_timestamp { get; set; }
        public TypeObjectPermission _type_object_permission { get; set; }
    }

    public class TVWriterJSON
    {
        public string id { get; set; }
        public Property property { get; set; }


        public TVWriterJSON()
        {

        }

        public TVWriterJSON(String json)
        {
            TVWriterJSON deserializedUser = new TVWriterJSON();
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
            DataContractJsonSerializer ser = new DataContractJsonSerializer(deserializedUser.GetType());
            deserializedUser = ser.ReadObject(ms) as TVWriterJSON;
            ms.Close();
            id = deserializedUser.id;
            property = deserializedUser.property;
            //return deserializedUser;
        }

        public String getDescription()
        {
            String result = null;
            TVWriter.Value15 v = property._common_topic_article.values.First();
            TVWriter.Value17 v2 = null;
            if (v != null)
            {
                v2 = v.property._common_document_text.values.First();
                if (v2 != null)
                {
                    result = v2.value;
                }
            }
            return result;
        }
    }
}
