#!/usr/bin/ruby
#---
# Excerpted from "RubyCocoa",
# published by The Pragmatic Bookshelf.
# Copyrights apply to this code. It may not be used to create training material, 
# courses, books, articles, and the like. Contact us if you are in doubt.
# We make no guarantees that this code is fit for any purpose. 
# Visit http://www.pragmaticprogrammer.com/titles/bmrc for more book information.
#---
#
# linguistics/iso639.rb - A hash of International 2- and 3-letter 
# ISO639-1 and ISO639-2 language codes. Each entry has two keys:
#
# [<tt>:codes</tt>]
#   All of the codes known for this language
# [<tt>:desc</tt>]
#   The English-language description of the language.
#

### A language-independent framework for adding linguistics functions to Ruby
### classes.
module Linguistics

	# Hash of ISO639 2- and 3-letter language codes
	LanguageCodes = {}

	# Read through the source for this file, capturing everything
	# between __END__ and __END_DATA__ tokens.
	inDataSection = false
	File::readlines( __FILE__ ).each {|line|
		case line
		when /^__END_DATA__$/
			inDataSection = false
			false
			
		when /^__END__$/
			inDataSection = true
			false
			
		else
			if inDataSection
				codes, desc = line[0,15].split(%r{/|\s+}), line[15...-1]
				codes.delete_if {|code| code.empty?}
				entry = {
					:desc	=> desc.strip,
					:codes	=> codes.dup,
				}
				codes.each {|code|
					raise "Duplicate language code #{code}:"\
						"(#{LanguageCodes[code][:desc]}})}" \
						if LanguageCodes.key?( code )
					LanguageCodes[ code.strip ] = entry
				}
			end
		end
	}
end

__END__
abk      ab    Abkhazian
ace            Achinese
ach            Acoli
ada            Adangme
aar      aa    Afar
afh            Afrihili
afr      af    Afrikaans
afa            Afro-Asiatic (Other)
aka            Akan
akk            Akkadian
alb/sqi  sq    Albanian
ale            Aleut
alg            Algonquian languages
tut            Altaic (Other)
amh      am    Amharic
apa            Apache languages
ara      ar    Arabic
arc            Aramaic
arp            Arapaho
arn            Araucanian
arw            Arawak
arm/hye  hy    Armenian
art            Artificial (Other)
asm      as    Assamese
ath            Athapascan languages
map            Austronesian (Other)
ava            Avaric
ave            Avestan
awa            Awadhi
aym      ay    Aymara
aze      az    Azerbaijani
nah            Aztec
ban            Balinese
bat            Baltic (Other)
bal            Baluchi
bam            Bambara
bai            Bamileke languages
bad            Banda
bnt            Bantu (Other)
bas            Basa
bak      ba    Bashkir
baq/eus  eu    Basque
bej            Beja
bem            Bemba
ben      bn    Bengali
ber            Berber (Other)
bho            Bhojpuri
bih      bh    Bihari
bik            Bikol
bin            Bini
bis      bi    Bislama
bra            Braj
bre      br    Breton
bug            Buginese
bul      bg    Bulgarian
bua            Buriat
bur/mya  my    Burmese
bel      be    Byelorussian
cad            Caddo
car            Carib
cat      ca    Catalan
cau            Caucasian (Other)
ceb            Cebuano
cel            Celtic (Other)
cai            Central American Indian (Other)
chg            Chagatai
cha            Chamorro
che            Chechen
chr            Cherokee
chy            Cheyenne
chb            Chibcha
chi/zho  zh    Chinese
chn            Chinook jargon
cho            Choctaw
chu            Church Slavic
chv            Chuvash
cop            Coptic
cor            Cornish
cos      co    Corsican
cre            Cree
mus            Creek
crp            Creoles and Pidgins (Other)
cpe            Creoles and Pidgins, English-based (Other)
cpf            Creoles and Pidgins, French-based (Other)
cpp            Creoles and Pidgins, Portuguese-based (Other)
cus            Cushitic (Other)
         hr    Croatian
ces/cze  cs    Czech
dak            Dakota
dan      da    Danish
del            Delaware
din            Dinka
div            Divehi
doi            Dogri
dra            Dravidian (Other)
dua            Duala
dut/nla  nl    Dutch
dum            Dutch, Middle (ca. 1050-1350)
dyu            Dyula
dzo      dz    Dzongkha
efi            Efik
egy            Egyptian (Ancient)
eka            Ekajuk
elx            Elamite
eng      en    English
enm            English, Middle (ca. 1100-1500)
ang            English, Old (ca. 450-1100)
esk            Eskimo (Other)
epo      eo    Esperanto
est      et    Estonian
ewe            Ewe
ewo            Ewondo
fan            Fang
fat            Fanti
fao      fo    Faroese
fij      fj    Fijian
fin      fi    Finnish
fiu            Finno-Ugrian (Other)
fon            Fon
fra/fre  fr    French
frm            French, Middle (ca. 1400-1600)
fro            French, Old (842- ca. 1400)
fry      fy    Frisian
ful            Fulah
gaa            Ga
gae/gdh        Gaelic (Scots)
glg      gl    Gallegan
lug            Ganda
gay            Gayo
gez            Geez
geo/kat  ka    Georgian
deu/ger  de    German
gmh            German, Middle High (ca. 1050-1500)
goh            German, Old High (ca. 750-1050)
gem            Germanic (Other)
gil            Gilbertese
gon            Gondi
got            Gothic
grb            Grebo
grc            Greek, Ancient (to 1453)
ell/gre  el    Greek, Modern (1453-)
kal      kl    Greenlandic
grn      gn    Guarani
guj      gu    Gujarati
hai            Haida
hau      ha    Hausa
haw            Hawaiian
heb      he    Hebrew
her            Herero
hil            Hiligaynon
him            Himachali
hin      hi    Hindi
hmo            Hiri Motu
hun      hu    Hungarian
hup            Hupa
iba            Iban
ice/isl  is    Icelandic
ibo            Igbo
ijo            Ijo
ilo            Iloko
inc            Indic (Other)
ine            Indo-European (Other)
ind      id    Indonesian
ina      ia    Interlingua (International Auxiliary language Association)
ile            Interlingue
iku      iu    Inuktitut
ipk      ik    Inupiak
ira            Iranian (Other)
gai/iri  ga    Irish
sga            Irish, Old (to 900)
mga            Irish, Middle (900 - 1200)
iro            Iroquoian languages
ita      it    Italian
jpn      ja    Japanese
jav/jaw  jv/jw Javanese
jrb            Judeo-Arabic
jpr            Judeo-Persian
kab            Kabyle
kac            Kachin
kam            Kamba
kan      kn    Kannada
kau            Kanuri
kaa            Kara-Kalpak
kar            Karen
kas      ks    Kashmiri
kaw            Kawi
kaz      kk    Kazakh
kha            Khasi
khm      km    Khmer
khi            Khoisan (Other)
kho            Khotanese
kik            Kikuyu
kin      rw    Kinyarwanda
kir      ky    Kirghiz
kom            Komi
kon            Kongo
kok            Konkani
kor      ko    Korean
kpe            Kpelle
kro            Kru
kua            Kuanyama
kum            Kumyk
kur      ku    Kurdish
kru            Kurukh
kus            Kusaie
kut            Kutenai
lad            Ladino
lah            Lahnda
lam            Lamba
oci      oc    Langue d'Oc (post 1500)
lao      lo    Lao
lat      la    Latin
lav      lv    Latvian
ltz            Letzeburgesch
lez            Lezghian
lin      ln    Lingala
lit      lt    Lithuanian
loz            Lozi
lub            Luba-Katanga
lui            Luiseno
lun            Lunda
luo            Luo (Kenya and Tanzania)
mac/mke  mk    Macedonian
mad            Madurese
mag            Magahi
mai            Maithili
mak            Makasar
mlg      mg    Malagasy
may/msa  ms    Malay
mal            Malayalam
mlt      ml    Maltese
man            Mandingo
mni            Manipuri
mno            Manobo languages
max            Manx
mao/mri  mi    Maori
mar      mr    Marathi
chm            Mari
mah            Marshall
mwr            Marwari
mas            Masai
myn            Mayan languages
men            Mende
mic            Micmac
min            Minangkabau
mis            Miscellaneous (Other)
moh            Mohawk
mol      mo    Moldavian
mkh            Mon-Kmer (Other)
lol            Mongo
mon      mn    Mongolian
mos            Mossi
mul            Multiple languages
mun            Munda languages
nau      na    Nauru
nav            Navajo
nde            Ndebele, North
nbl            Ndebele, South
ndo            Ndongo
nep      ne    Nepali
new            Newari
nic            Niger-Kordofanian (Other)
ssa            Nilo-Saharan (Other)
niu            Niuean
non            Norse, Old
nai            North American Indian (Other)
nor      no    Norwegian
nno            Norwegian (Nynorsk)
nub            Nubian languages
nym            Nyamwezi
nya            Nyanja
nyn            Nyankole
nyo            Nyoro
nzi            Nzima
oji            Ojibwa
ori      or    Oriya
orm      om    Oromo
osa            Osage
oss            Ossetic
oto            Otomian languages
pal            Pahlavi
pau            Palauan
pli            Pali
pam            Pampanga
pag            Pangasinan
pan      pa    Panjabi
pap            Papiamento
paa            Papuan-Australian (Other)
fas/per  fa    Persian
peo            Persian, Old (ca 600 - 400 B.C.)
phn            Phoenician
pol      pl    Polish
pon            Ponape
por      pt    Portuguese
pra            Prakrit languages
pro            Provencal, Old (to 1500)
pus      ps    Pushto
que      qu    Quechua
roh      rm    Rhaeto-Romance
raj            Rajasthani
rar            Rarotongan
roa            Romance (Other)
ron/rum  ro    Romanian
rom            Romany
run      rn    Rundi
rus      ru    Russian
sal            Salishan languages
sam            Samaritan Aramaic
smi            Sami languages
smo      sm    Samoan
sad            Sandawe
sag      sg    Sango
san      sa    Sanskrit
srd            Sardinian
sco            Scots
sel            Selkup
sem            Semitic (Other)
         sr    Serbian
scr      sh    Serbo-Croatian
srr            Serer
shn            Shan
sna      sn    Shona
sid            Sidamo
bla            Siksika
snd      sd    Sindhi
sin      si    Singhalese
sit            Sino-Tibetan (Other)
sio            Siouan languages
sla            Slavic (Other)
         ss    Siswati
slk/slo  sk    Slovak
slv      sl    Slovenian
sog            Sogdian
som      so    Somali
son            Songhai
wen            Sorbian languages
nso            Sotho, Northern
sot      st    Sotho, Southern
sai            South American Indian (Other)
esl/spa  es    Spanish
suk            Sukuma
sux            Sumerian
sun      su    Sudanese
sus            Susu
swa      sw    Swahili
ssw            Swazi
sve/swe  sv    Swedish
syr            Syriac
tgl      tl    Tagalog
tah            Tahitian
tgk      tg    Tajik
tmh            Tamashek
tam      ta    Tamil
tat      tt    Tatar
tel      te    Telugu
ter            Tereno
tha      th    Thai
bod/tib  bo    Tibetan
tig            Tigre
tir      ti    Tigrinya
tem            Timne
tiv            Tivi
tli            Tlingit
tog      to    Tonga (Nyasa)
ton            Tonga (Tonga Islands)
tru            Truk
tsi            Tsimshian
tso      ts    Tsonga
tsn      tn    Tswana
tum            Tumbuka
tur      tr    Turkish
ota            Turkish, Ottoman (1500 - 1928)
tuk      tk    Turkmen
tyv            Tuvinian
twi      tw    Twi
uga            Ugaritic
uig      ug    Uighur
ukr      uk    Ukrainian
umb            Umbundu
und            Undetermined
urd      ur    Urdu
uzb      uz    Uzbek
vai            Vai
ven            Venda
vie      vi    Vietnamese
vol      vo    Volap�k
vot            Votic
wak            Wakashan languages
wal            Walamo
war            Waray
was            Washo
cym/wel  cy    Welsh
wol      wo    Wolof
xho      xh    Xhosa
sah            Yakut
yao            Yao
yap            Yap
yid      yi    Yiddish
yor      yo    Yoruba
zap            Zapotec
zen            Zenaga
zha      za    Zhuang
zul      zu    Zulu
zun            Zuni
__END_DATA__
