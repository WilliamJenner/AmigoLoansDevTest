SELECT name 
FROM engineers,
	(SELECT engineer_id
	FROM rota 
	WHERE (shift != date('now') AND date('now', '-1 day'))
		AND (shift BETWEEN date('now','-14 day') AND date('now'))) AS eligible
WHERE Engineers.Id = eligible.Engineer_Id
/* Will get a list of all engineers eligible to work 
(has not worked today already, or yesterday) within the last 2 weeks*/