-- Query to remove Forgien key constraint for Guestbook comments 

ALTER TABLE LatestSummary
DROP CONSTRAINT fk_Comments_Createdby