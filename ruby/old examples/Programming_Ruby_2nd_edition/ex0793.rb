# Sample code from Programing Ruby, page 433
        %w{ ant bear cat}.any? {|word| word.length >= 3}
        %w{ ant bear cat}.any? {|word| word.length >= 4}
        [ nil, true, 99 ].any?
