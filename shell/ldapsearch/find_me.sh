#!/bin/bash

#
# this came from the following links:
#
# this one was more helpful
# https://tylersguides.com/guides/search-active-directory-ldapsearch/
#
# https://devconnected.com/how-to-search-ldap-using-ldapsearch-examples/
#
# https://serverfault.com/questions/132026/listing-group-members-using-ldapsearch
#




#
# ldapsearch -H ldaps://dc.example.com -x -W -D "user@example.com" \ 
#    -b "dc=example,dc=com" "(filter)" "attr1" "attr2"
#
# explaining what's going on here
#
# -H the uri of the directory server you are querying
#
# -x use simple auth instead of SASL
#
# -W prompt your for your password
#
# -D the DN of the user you are authenticating with.  when querying AD, this will be your AD user name @ your domain
#
# -b where in the directory to start your search
# hint: just use the root of the domain
#
# filter the ldap serach filter used to find entries
# I'll probably not use this very often if ever
#
# attr the attributes you wish to display
# if you wanna be selective in what comes back
#

# looking for a computer object
# ldapsearch -x -b "dc=tntech,dc=edu" -H ldap://one.tntech.edu -D "blakesmith2@tntech.edu" -W "(sAMAccountName=clem110-d10)"

# ldapsearch -x -b "dc=tntech,dc=edu" -H ldap://one.tntech.edu -D "blakesmith2@tntech.edu" -W "(sAMAccountName=blakesmith)"

# gets the group membership

ldapsearch -x -b "dc=tntech,dc=edu" -H ldap://one.tntech.edu -D "blakesmith2@tntech.edu" -W "(memberUid=ITS ASSG)"

# testing to see if I can get a nested group list of all users in portalfacultystaff
# it works, but it doesn't show nested groups' members

# ldapsearch -x -b "cn=PortalFacultyStaff,ou=TTU Groups,dc=tntech,dc=edu" -H ldap://one.tntech.edu -D "blakesmith2@tntech.edu" -W "member"
