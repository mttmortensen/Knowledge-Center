UPDATE LogEntries
SET TagId = Tags.TagId
FROM LogEntries
JOIN Tags ON LOWER(LogEntries.Tag) = LOWER(Tags.Name);
