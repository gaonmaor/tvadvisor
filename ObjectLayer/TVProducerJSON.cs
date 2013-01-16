using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Json;

namespace TVProducer
{
    public class Value2
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
        public List<Value2> values { get; set; }
        public double count { get; set; }
    }

    public class Value3
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
        public List<Value3> values { get; set; }
        public double count { get; set; }
    }

    public class Value4
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

    public class TypeObjectType
    {
        public string valuetype { get; set; }
        public List<Value5> values { get; set; }
        public double count { get; set; }
    }

    public class Property2
    {
        public CommonDocumentSourceUri _common_document_source_uri { get; set; }
        public CommonDocumentText _common_document_text { get; set; }
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

    public class CommonTopicArticle
    {
        public string valuetype { get; set; }
        public List<Value> values { get; set; }
        public double count { get; set; }
    }

    public class Value6
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string id { get; set; }
    }

    public class CommonTopicNotableFor
    {
        public string valuetype { get; set; }
        public List<Value6> values { get; set; }
        public double count { get; set; }
    }

    public class Value7
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string id { get; set; }
        public string timestamp { get; set; }
    }

    public class CommonTopicNotableTypes
    {
        public string valuetype { get; set; }
        public List<Value7> values { get; set; }
        public double count { get; set; }
    }

    public class Value8
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

    public class FictionalUniverseFictionalUniverseCreatorFictionalUniversesCreated
    {
        public string valuetype { get; set; }
        public List<Value9> values { get; set; }
        public double count { get; set; }
    }

    public class Value10
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string id { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
    }

    public class FilmDirectorFilm
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

    public class FilmWriterFilm
    {
        public string valuetype { get; set; }
        public List<Value11> values { get; set; }
        public double count { get; set; }
    }

    public class Value12
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
        public List<Value12> values { get; set; }
        public double count { get; set; }
    }

    public class Value14
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
        public List<Value14> values { get; set; }
        public double count { get; set; }
    }

    public class Value15
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
        public List<Value15> values { get; set; }
        public double count { get; set; }
    }

    public class Value16
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
        public List<Value16> values { get; set; }
        public double count { get; set; }
    }

    public class Property3
    {
        public EducationEducationInstitution _education_education_institution { get; set; }
        public TypeObjectAttribution2 _type_object_attribution { get; set; }
        public TypeObjectType2 _type_object_type { get; set; }
    }

    public class Value13
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string id { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
        public Property3 property { get; set; }
    }

    public class PeoplePersonEducation
    {
        public string valuetype { get; set; }
        public List<Value13> values { get; set; }
        public double count { get; set; }
    }

    public class Value17
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

    public class PeoplePersonNationality
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

    public class PeoplePersonPlaceOfBirth
    {
        public string valuetype { get; set; }
        public List<Value19> values { get; set; }
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

    public class PeoplePersonProfession
    {
        public string valuetype { get; set; }
        public List<Value20> values { get; set; }
        public double count { get; set; }
    }

    public class Value22
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string id { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
    }

    public class TvTvGuestRoleCharacter
    {
        public string valuetype { get; set; }
        public List<Value22> values { get; set; }
        public double count { get; set; }
    }

    public class Value23
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
        public List<Value23> values { get; set; }
        public double count { get; set; }
    }

    public class Value24
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
        public List<Value24> values { get; set; }
        public double count { get; set; }
    }

    public class Value25
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string id { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
    }

    public class TvTvGuestRoleEpisodesAppearedIn
    {
        public string valuetype { get; set; }
        public List<Value25> values { get; set; }
        public double count { get; set; }
    }

    public class Property4
    {
        public TvTvGuestRoleCharacter _tv_tv_guest_role_character { get; set; }
        public TypeObjectAttribution3 _type_object_attribution { get; set; }
        public TypeObjectType3 _type_object_type { get; set; }
        public TvTvGuestRoleEpisodesAppearedIn _tv_tv_guest_role_episodes_appeared_in { get; set; }
    }

    public class Value21
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string id { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
        public Property4 property { get; set; }
    }

    public class TvTvActorGuestRoles
    {
        public string valuetype { get; set; }
        public List<Value21> values { get; set; }
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

    public class TvTvDirectorEpisodesDirected
    {
        public string valuetype { get; set; }
        public List<Value26> values { get; set; }
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

    public class TvTvProducerTermProgram
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

    public class TypeObjectAttribution4
    {
        public string valuetype { get; set; }
        public List<Value29> values { get; set; }
        public double count { get; set; }
    }

    public class Value30
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
        public List<Value30> values { get; set; }
        public double count { get; set; }
    }

    public class Property5
    {
        public TvTvProducerTermProgram _tv_tv_producer_term_program { get; set; }
        public TypeObjectAttribution4 _type_object_attribution { get; set; }
        public TypeObjectType4 _type_object_type { get; set; }
    }

    public class Value27
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string id { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
        public Property5 property { get; set; }
    }

    public class TvTvProducerProgramsProduced
    {
        public string valuetype { get; set; }
        public List<Value27> values { get; set; }
        public double count { get; set; }
    }

    public class Value31
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
        public List<Value31> values { get; set; }
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

    public class TvTvWriterEpisodesWritten
    {
        public string valuetype { get; set; }
        public List<Value32> values { get; set; }
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

    public class TvTvProgramWriterRelationshipTvProgram
    {
        public string valuetype { get; set; }
        public List<Value34> values { get; set; }
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

    public class TypeObjectAttribution5
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

    public class TypeObjectType5
    {
        public string valuetype { get; set; }
        public List<Value36> values { get; set; }
        public double count { get; set; }
    }

    public class Value37
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string value { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
    }

    public class TvTvProgramWriterRelationshipEndDate
    {
        public string valuetype { get; set; }
        public List<Value37> values { get; set; }
        public double count { get; set; }
    }

    public class Value38
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string value { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
    }

    public class TvTvProgramWriterRelationshipStartDate
    {
        public string valuetype { get; set; }
        public List<Value38> values { get; set; }
        public double count { get; set; }
    }

    public class Property6
    {
        public TvTvProgramWriterRelationshipTvProgram _tv_tv_program_writer_relationship_tv_program { get; set; }
        public TypeObjectAttribution5 _type_object_attribution { get; set; }
        public TypeObjectType5 _type_object_type { get; set; }
        public TvTvProgramWriterRelationshipEndDate _tv_tv_program_writer_relationship_end_date { get; set; }
        public TvTvProgramWriterRelationshipStartDate _tv_tv_program_writer_relationship_start_date { get; set; }
    }

    public class Value33
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string id { get; set; }
        public string creator { get; set; }
        public string timestamp { get; set; }
        public Property6 property { get; set; }
    }

    public class TvTvWriterTvPrograms
    {
        public string valuetype { get; set; }
        public List<Value33> values { get; set; }
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

    public class TypeObjectAttribution6
    {
        public string valuetype { get; set; }
        public List<Value39> values { get; set; }
        public double count { get; set; }
    }

    public class Value40
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
        public List<Value40> values { get; set; }
        public double count { get; set; }
    }

    public class Value41
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string value { get; set; }
    }

    public class TypeObjectMid
    {
        public string valuetype { get; set; }
        public List<Value41> values { get; set; }
        public double count { get; set; }
    }

    public class Value42
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
        public List<Value42> values { get; set; }
        public double count { get; set; }
    }

    public class Value43
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
        public List<Value43> values { get; set; }
        public double count { get; set; }
    }

    public class Value44
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string id { get; set; }
    }

    public class CommonTopicNotableProperties
    {
        public string valuetype { get; set; }
        public List<Value44> values { get; set; }
        public double count { get; set; }
    }

    public class Value45
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string value { get; set; }
    }

    public class TypeObjectGuid
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
        public string timestamp { get; set; }
    }

    public class TypeObjectCreator
    {
        public string valuetype { get; set; }
        public List<Value46> values { get; set; }
        public double count { get; set; }
    }

    public class Value47
    {
        public string text { get; set; }
        public string lang { get; set; }
        public string value { get; set; }
    }

    public class TypeObjectTimestamp
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
    }

    public class TypeObjectPermission
    {
        public string valuetype { get; set; }
        public List<Value48> values { get; set; }
        public double count { get; set; }
    }

    public class Property
    {
        public CommonTopicArticle _common_topic_article { get; set; }
        public CommonTopicNotableFor _common_topic_notable_for { get; set; }
        public CommonTopicNotableTypes _common_topic_notable_types { get; set; }
        public CommonTopicTopicEquivalentWebpage _common_topic_topic_equivalent_webpage { get; set; }
        public FictionalUniverseFictionalUniverseCreatorFictionalUniversesCreated _fictional_universe_fictional_universe_creator_fictional_universes_created { get; set; }
        public FilmDirectorFilm _film_director_film { get; set; }
        public FilmWriterFilm _film_writer_film { get; set; }
        public PeoplePersonDateOfBirth _people_person_date_of_birth { get; set; }
        public PeoplePersonEducation _people_person_education { get; set; }
        public PeoplePersonGender _people_person_gender { get; set; }
        public PeoplePersonNationality _people_person_nationality { get; set; }
        public PeoplePersonPlaceOfBirth _people_person_place_of_birth { get; set; }
        public PeoplePersonProfession _people_person_profession { get; set; }
        public TvTvActorGuestRoles _tv_tv_actor_guest_roles { get; set; }
        public TvTvDirectorEpisodesDirected _tv_tv_director_episodes_directed { get; set; }
        public TvTvProducerProgramsProduced _tv_tv_producer_programs_produced { get; set; }
        public TvTvProgramCreatorProgramsCreated _tv_tv_program_creator_programs_created { get; set; }
        public TvTvWriterEpisodesWritten _tv_tv_writer_episodes_written { get; set; }
        public TvTvWriterTvPrograms _tv_tv_writer_tv_programs { get; set; }
        public TypeObjectAttribution6 _type_object_attribution { get; set; }
        public TypeObjectKey _type_object_key { get; set; }
        public TypeObjectMid _type_object_mid { get; set; }
        public TypeObjectName _type_object_name { get; set; }
        public TypeObjectType6 _type_object_type { get; set; }
        public CommonTopicNotableProperties _common_topic_notable_properties { get; set; }
        public TypeObjectGuid _type_object_guid { get; set; }
        public TypeObjectCreator _type_object_creator { get; set; }
        public TypeObjectTimestamp _type_object_timestamp { get; set; }
        public TypeObjectPermission _type_object_permission { get; set; }
    }

    public class TVProducerJSON
    {
        public string id { get; set; }
        public Property property { get; set; }


        public TVProducerJSON()
        {

        }

        public TVProducerJSON(String json)
        {
            TVProducerJSON deserializedUser = new TVProducerJSON();
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
            DataContractJsonSerializer ser = new DataContractJsonSerializer(deserializedUser.GetType());
            deserializedUser = ser.ReadObject(ms) as TVProducerJSON;
            ms.Close();
            id = deserializedUser.id;
            property = deserializedUser.property;
            //return deserializedUser;
        }

        public String getDescription()
        {
            String result = null;
            TVProducer.Value v = property._common_topic_article.values.First();
            TVProducer.Value3 v2 = null;
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

        public String getMID()
        {
            String result = id;
            return result;
        }
    }

}
