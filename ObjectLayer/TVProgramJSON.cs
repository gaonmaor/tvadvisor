using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Json;

namespace TVProgram
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

public class AwardAwardNominationAwardNominee
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
    public AwardAwardNominationAwardNominee _award_award_nomination_award_nominee { get; set; }
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

public class AwardAwardNominatedWorkAwardNominations
{
    public string valuetype { get; set; }
    public List<Value> values { get; set; }
    public double count { get; set; }
}

public class Value7
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
    public List<Value7> values { get; set; }
    public double count { get; set; }
}

public class Value9
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

public class CommonDocumentText
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
    public CommonDocumentSourceUri _common_document_source_uri { get; set; }
    public CommonDocumentText _common_document_text { get; set; }
    public TypeObjectAttribution2 _type_object_attribution { get; set; }
    public TypeObjectType2 _type_object_type { get; set; }
}

public class Value8
{
    public string text { get; set; }
    public string lang { get; set; }
    public string id { get; set; }
    public string creator { get; set; }
    public string timestamp { get; set; }
    public Property3 property { get; set; }
}

public class CommonTopicArticle
{
    public string valuetype { get; set; }
    public List<Value8> values { get; set; }
    public double count { get; set; }
}

public class Value13
{
    public string text { get; set; }
    public string lang { get; set; }
    public string id { get; set; }
}

public class CommonTopicNotableFor
{
    public string valuetype { get; set; }
    public List<Value13> values { get; set; }
    public double count { get; set; }
}

public class Value14
{
    public string text { get; set; }
    public string lang { get; set; }
    public string id { get; set; }
    public string timestamp { get; set; }
}

public class CommonTopicNotableTypes
{
    public string valuetype { get; set; }
    public List<Value14> values { get; set; }
    public double count { get; set; }
}

public class Value15
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
    public List<Value15> values { get; set; }
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

public class CommonTopicSocialMediaPresence
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

public class CommonTopicTopicEquivalentWebpage
{
    public string valuetype { get; set; }
    public List<Value17> values { get; set; }
    public double count { get; set; }
}

public class Value18
{
    public string text { get; set; }
    public string lang { get; set; }
    public string value { get; set; }
    public string creator { get; set; }
    public string timestamp { get; set; }
}

public class CommonTopicTopicalWebpage
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

public class MediaCommonNetflixTitleNetflixGenres
{
    public string valuetype { get; set; }
    public List<Value19> values { get; set; }
    public double count { get; set; }
}

public class Value20
{
    public string text { get; set; }
    public string lang { get; set; }
    public string value { get; set; }
    public string creator { get; set; }
    public string timestamp { get; set; }
}

public class TvTvProgramAirDateOfFirstEpisode
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
    public string creator { get; set; }
    public string timestamp { get; set; }
}

public class TvTvProgramCountryOfOrigin
{
    public string valuetype { get; set; }
    public List<Value21> values { get; set; }
    public double count { get; set; }
}

public class Value22
{
    public string text { get; set; }
    public string lang { get; set; }
    public bool value { get; set; }
    public string creator { get; set; }
    public string timestamp { get; set; }
}

public class TvTvProgramCurrentlyInProduction
{
    public string valuetype { get; set; }
    public List<Value22> values { get; set; }
    public double count { get; set; }
}

public class Value23
{
    public string text { get; set; }
    public string lang { get; set; }
    public double value { get; set; }
    public string creator { get; set; }
    public string timestamp { get; set; }
}

public class TvTvProgramEpisodeRunningTime
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

public class TvTvProgramEpisodes
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

public class TvTvProgramGenre
{
    public string valuetype { get; set; }
    public List<Value25> values { get; set; }
    public double count { get; set; }
}

public class Value26
{
    public string text { get; set; }
    public string lang { get; set; }
    public double value { get; set; }
    public string creator { get; set; }
    public string timestamp { get; set; }
}

public class TvTvProgramNumberOfEpisodes
{
    public string valuetype { get; set; }
    public List<Value26> values { get; set; }
    public double count { get; set; }
}

public class Value27
{
    public string text { get; set; }
    public string lang { get; set; }
    public double value { get; set; }
    public string creator { get; set; }
    public string timestamp { get; set; }
}

public class TvTvProgramNumberOfSeasons
{
    public string valuetype { get; set; }
    public List<Value27> values { get; set; }
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

public class TvTvNetworkDurationNetwork
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

public class TypeObjectAttribution3
{
    public string valuetype { get; set; }
    public List<Value30> values { get; set; }
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

public class TypeObjectType3
{
    public string valuetype { get; set; }
    public List<Value31> values { get; set; }
    public double count { get; set; }
}

public class Property4
{
    public TvTvNetworkDurationNetwork _tv_tv_network_duration_network { get; set; }
    public TypeObjectAttribution3 _type_object_attribution { get; set; }
    public TypeObjectType3 _type_object_type { get; set; }
}

public class Value28
{
    public string text { get; set; }
    public string lang { get; set; }
    public string id { get; set; }
    public string creator { get; set; }
    public string timestamp { get; set; }
    public Property4 property { get; set; }
}

public class TvTvProgramOriginalNetwork
{
    public string valuetype { get; set; }
    public List<Value28> values { get; set; }
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

public class TvTvProgramProgramCreator
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

public class TvRegularTvAppearanceActor
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

public class TypeObjectAttribution4
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

public class TypeObjectType4
{
    public string valuetype { get; set; }
    public List<Value36> values { get; set; }
    public double count { get; set; }
}

public class Property5
{
    public TvRegularTvAppearanceActor _tv_regular_tv_appearance_actor { get; set; }
    public TypeObjectAttribution4 _type_object_attribution { get; set; }
    public TypeObjectType4 _type_object_type { get; set; }
}

public class Value33
{
    public string text { get; set; }
    public string lang { get; set; }
    public string id { get; set; }
    public string creator { get; set; }
    public string timestamp { get; set; }
    public Property5 property { get; set; }
}

public class TvTvProgramRegularCast
{
    public string valuetype { get; set; }
    public List<Value33> values { get; set; }
    public double count { get; set; }
}

public class Value38
{
    public string text { get; set; }
    public string lang { get; set; }
    public string id { get; set; }
    public string creator { get; set; }
    public string timestamp { get; set; }
}

public class TvTvRegularPersonalAppearanceAppearanceType
{
    public string valuetype { get; set; }
    public List<Value38> values { get; set; }
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

public class TvTvRegularPersonalAppearancePerson
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

public class TypeObjectAttribution5
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

public class TypeObjectType5
{
    public string valuetype { get; set; }
    public List<Value41> values { get; set; }
    public double count { get; set; }
}

public class Property6
{
    public TvTvRegularPersonalAppearanceAppearanceType _tv_tv_regular_personal_appearance_appearance_type { get; set; }
    public TvTvRegularPersonalAppearancePerson _tv_tv_regular_personal_appearance_person { get; set; }
    public TypeObjectAttribution5 _type_object_attribution { get; set; }
    public TypeObjectType5 _type_object_type { get; set; }
}

public class Value37
{
    public string text { get; set; }
    public string lang { get; set; }
    public string id { get; set; }
    public string creator { get; set; }
    public string timestamp { get; set; }
    public Property6 property { get; set; }
}

public class TvTvProgramRegularPersonalAppearances
{
    public string valuetype { get; set; }
    public List<Value37> values { get; set; }
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

public class TvTvProgramSeasons
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

public class TvTvProgramSpinOffs
{
    public string valuetype { get; set; }
    public List<Value43> values { get; set; }
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

public class TvTvProducerTermProducer
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

public class TypeObjectAttribution6
{
    public string valuetype { get; set; }
    public List<Value46> values { get; set; }
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

public class TypeObjectType6
{
    public string valuetype { get; set; }
    public List<Value47> values { get; set; }
    public double count { get; set; }
}

public class Property7
{
    public TvTvProducerTermProducer _tv_tv_producer_term_producer { get; set; }
    public TypeObjectAttribution6 _type_object_attribution { get; set; }
    public TypeObjectType6 _type_object_type { get; set; }
}

public class Value44
{
    public string text { get; set; }
    public string lang { get; set; }
    public string id { get; set; }
    public string creator { get; set; }
    public string timestamp { get; set; }
    public Property7 property { get; set; }
}

public class TvTvProgramTvProducer
{
    public string valuetype { get; set; }
    public List<Value44> values { get; set; }
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

public class TypeObjectAttribution7
{
    public string valuetype { get; set; }
    public List<Value48> values { get; set; }
    public double count { get; set; }
}

public class Value49
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
    public List<Value49> values { get; set; }
    public double count { get; set; }
}

public class Value50
{
    public string text { get; set; }
    public string lang { get; set; }
    public string value { get; set; }
}

public class TypeObjectMid
{
    public string valuetype { get; set; }
    public List<Value50> values { get; set; }
    public double count { get; set; }
}

public class Value51
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

public class TypeObjectType7
{
    public string valuetype { get; set; }
    public List<Value52> values { get; set; }
    public double count { get; set; }
}

public class Value53
{
    public string text { get; set; }
    public string lang { get; set; }
    public string id { get; set; }
}

public class CommonTopicNotableProperties
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
}

public class TypeObjectGuid
{
    public string valuetype { get; set; }
    public List<Value54> values { get; set; }
    public double count { get; set; }
}

public class Value55
{
    public string text { get; set; }
    public string lang { get; set; }
    public string id { get; set; }
    public string timestamp { get; set; }
}

public class TypeObjectCreator
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
}

public class TypeObjectTimestamp
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
}

public class TypeObjectPermission
{
    public string valuetype { get; set; }
    public List<Value57> values { get; set; }
    public double count { get; set; }
}

public class Property
{
    public AwardAwardNominatedWorkAwardNominations _award_award_nominated_work_award_nominations { get; set; }
    public CommonTopicAlias _common_topic_alias { get; set; }
    public CommonTopicArticle _common_topic_article { get; set; }
    public CommonTopicNotableFor _common_topic_notable_for { get; set; }
    public CommonTopicNotableTypes _common_topic_notable_types { get; set; }
    public CommonTopicOfficialWebsite _common_topic_official_website { get; set; }
    public CommonTopicSocialMediaPresence _common_topic_social_media_presence { get; set; }
    public CommonTopicTopicEquivalentWebpage _common_topic_topic_equivalent_webpage { get; set; }
    public CommonTopicTopicalWebpage _common_topic_topical_webpage { get; set; }
    public MediaCommonNetflixTitleNetflixGenres _media_common_netflix_title_netflix_genres { get; set; }
    public TvTvProgramAirDateOfFirstEpisode _tv_tv_program_air_date_of_first_episode { get; set; }
    public TvTvProgramCountryOfOrigin _tv_tv_program_country_of_origin { get; set; }
    public TvTvProgramCurrentlyInProduction _tv_tv_program_currently_in_production { get; set; }
    public TvTvProgramEpisodeRunningTime _tv_tv_program_episode_running_time { get; set; }
    public TvTvProgramEpisodes _tv_tv_program_episodes { get; set; }
    public TvTvProgramGenre _tv_tv_program_genre { get; set; }
    public TvTvProgramNumberOfEpisodes _tv_tv_program_number_of_episodes { get; set; }
    public TvTvProgramNumberOfSeasons _tv_tv_program_number_of_seasons { get; set; }
    public TvTvProgramOriginalNetwork _tv_tv_program_original_network { get; set; }
    public TvTvProgramProgramCreator _tv_tv_program_program_creator { get; set; }
    public TvTvProgramRegularCast _tv_tv_program_regular_cast { get; set; }
    public TvTvProgramRegularPersonalAppearances _tv_tv_program_regular_personal_appearances { get; set; }
    public TvTvProgramSeasons _tv_tv_program_seasons { get; set; }
    public TvTvProgramSpinOffs _tv_tv_program_spin_offs { get; set; }
    public TvTvProgramTvProducer _tv_tv_program_tv_producer { get; set; }
    public TypeObjectAttribution7 _type_object_attribution { get; set; }
    public TypeObjectKey _type_object_key { get; set; }
    public TypeObjectMid _type_object_mid { get; set; }
    public TypeObjectName _type_object_name { get; set; }
    public TypeObjectType7 _type_object_type { get; set; }
    public CommonTopicNotableProperties _common_topic_notable_properties { get; set; }
    public TypeObjectGuid _type_object_guid { get; set; }
    public TypeObjectCreator _type_object_creator { get; set; }
    public TypeObjectTimestamp _type_object_timestamp { get; set; }
    public TypeObjectPermission _type_object_permission { get; set; }
}

    public class TVProgramJSON
    {
        public string id { get; set; }
        public Property property { get; set; }


        public TVProgramJSON()
        {

        }

        public TVProgramJSON(String json)
        {
            TVProgramJSON deserializedUser = new TVProgramJSON();
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
            DataContractJsonSerializer ser = new DataContractJsonSerializer(deserializedUser.GetType());
            deserializedUser = ser.ReadObject(ms) as TVProgramJSON;
            ms.Close();
            id = deserializedUser.id;
            property = deserializedUser.property;
            //return deserializedUser;
        }

        public String getDescription()
        {
            String result = null;
            TVProgram.Value8 v = property._common_topic_article.values.First();
            TVProgram.Value10 v2 = null;
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

        public List<String> getActorIDs()
        {
            TvTvProgramRegularCast cast = property._tv_tv_program_regular_cast;
            List<Value33> values = cast.values;//list of actors
            List<String> results = new List<String>();
            Value33 v = null;
            //String result = null;
            for (int i = 0; i < values.Count; i++)
            {
                v = values[i];
                if (v != null)
                {
                    //result = v.text;
                    List<Value34> p = v.property._tv_regular_tv_appearance_actor.values;
                    Value34 v2 = p.First();
                    if (v2 != null)
                    {
                        results.Add(v2.id);
                    }
                }
            }

            return results;
        }

        public List<String> getActorNames()
        {
            TvTvProgramRegularCast cast = property._tv_tv_program_regular_cast;
            List<Value33> values = cast.values;//list of actors
            List<String> results = new List<String>();
            Value33 v = null;
            //String result = null;
            for (int i = 0; i < values.Count; i++)
            {
                v = values[i];
                if (v != null)
                {
                    //result = v.text;
                    List<Value34> p = v.property._tv_regular_tv_appearance_actor.values;
                    Value34 v2 = p.First();
                    if (v2 != null)
                    {
                        results.Add(v2.text);
                    }
                }
            }

            return results;
        }

        public String getMID()
        {
            String result = id;
            return result;
        }
    }

}
