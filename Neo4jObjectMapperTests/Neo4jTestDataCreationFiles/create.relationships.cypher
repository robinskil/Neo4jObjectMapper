UNWIND [{start: {_id:2}, end: {_id:5}, properties:{}}, {start: {_id:3}, end: {_id:4}, properties:{}}, {start: {_id:9}, end: {_id:11}, properties:{}}, {start: {_id:10}, end: {_id:12}, properties:{}}, {start: {_id:16}, end: {_id:17}, properties:{}}, {start: {_id:21}, end: {_id:23}, properties:{}}, {start: {_id:22}, end: {_id:24}, properties:{}}, {start: {_id:29}, end: {_id:31}, properties:{}}, {start: {_id:30}, end: {_id:32}, properties:{}}, {start: {_id:35}, end: {_id:37}, properties:{}}, {start: {_id:36}, end: {_id:38}, properties:{}}, {start: {_id:40}, end: {_id:42}, properties:{}}, {start: {_id:41}, end: {_id:43}, properties:{}}, {start: {_id:47}, end: {_id:49}, properties:{}}, {start: {_id:48}, end: {_id:50}, properties:{}}, {start: {_id:54}, end: {_id:56}, properties:{}}, {start: {_id:55}, end: {_id:57}, properties:{}}, {start: {_id:60}, end: {_id:62}, properties:{}}, {start: {_id:61}, end: {_id:63}, properties:{}}, {start: {_id:66}, end: {_id:57}, properties:{}}] AS row
MATCH (start:`UNIQUE IMPORT LABEL`{`UNIQUE IMPORT ID`: row.start._id})
MATCH (end:`UNIQUE IMPORT LABEL`{`UNIQUE IMPORT ID`: row.end._id})
CREATE (start)-[r:IS_IN]->(end) SET r += row.properties;
UNWIND [{start: {_id:0}, end: {_id:3}, properties:{}}, {start: {_id:7}, end: {_id:10}, properties:{}}, {start: {_id:15}, end: {_id:16}, properties:{}}, {start: {_id:19}, end: {_id:22}, properties:{}}, {start: {_id:27}, end: {_id:30}, properties:{}}, {start: {_id:33}, end: {_id:36}, properties:{}}, {start: {_id:39}, end: {_id:41}, properties:{}}, {start: {_id:45}, end: {_id:48}, properties:{}}, {start: {_id:52}, end: {_id:55}, properties:{}}, {start: {_id:59}, end: {_id:61}, properties:{}}, {start: {_id:65}, end: {_id:66}, properties:{}}, {start: {_id:67}, end: {_id:66}, properties:{}}] AS row
MATCH (start:`UNIQUE IMPORT LABEL`{`UNIQUE IMPORT ID`: row.start._id})
MATCH (end:`UNIQUE IMPORT LABEL`{`UNIQUE IMPORT ID`: row.end._id})
CREATE (start)-[r:DESTINATION]->(end) SET r += row.properties;
UNWIND [{start: {_id:4}, end: {_id:6}, properties:{}}, {start: {_id:5}, end: {_id:6}, properties:{}}, {start: {_id:11}, end: {_id:13}, properties:{}}, {start: {_id:12}, end: {_id:13}, properties:{}}, {start: {_id:17}, end: {_id:6}, properties:{}}, {start: {_id:23}, end: {_id:25}, properties:{}}, {start: {_id:24}, end: {_id:25}, properties:{}}, {start: {_id:31}, end: {_id:25}, properties:{}}, {start: {_id:32}, end: {_id:25}, properties:{}}, {start: {_id:37}, end: {_id:25}, properties:{}}, {start: {_id:38}, end: {_id:25}, properties:{}}, {start: {_id:42}, end: {_id:25}, properties:{}}, {start: {_id:43}, end: {_id:44}, properties:{}}, {start: {_id:49}, end: {_id:25}, properties:{}}, {start: {_id:50}, end: {_id:51}, properties:{}}, {start: {_id:56}, end: {_id:25}, properties:{}}, {start: {_id:57}, end: {_id:25}, properties:{}}, {start: {_id:62}, end: {_id:64}, properties:{}}, {start: {_id:63}, end: {_id:51}, properties:{}}] AS row
MATCH (start:`UNIQUE IMPORT LABEL`{`UNIQUE IMPORT ID`: row.start._id})
MATCH (end:`UNIQUE IMPORT LABEL`{`UNIQUE IMPORT ID`: row.end._id})
CREATE (start)-[r:EXISTS_IN]->(end) SET r += row.properties;
UNWIND [{start: {_id:67}, end: {_id:68}, properties:{}}] AS row
MATCH (start:`UNIQUE IMPORT LABEL`{`UNIQUE IMPORT ID`: row.start._id})
MATCH (end:`UNIQUE IMPORT LABEL`{`UNIQUE IMPORT ID`: row.end._id})
CREATE (start)-[r:DESTINATION]->(end) SET r += row.properties;
UNWIND [{start: {_id:1}, end: {_id:6}, properties:{}}, {start: {_id:8}, end: {_id:14}, properties:{}}, {start: {_id:1}, end: {_id:18}, properties:{}}, {start: {_id:28}, end: {_id:25}, properties:{}}, {start: {_id:34}, end: {_id:25}, properties:{}}, {start: {_id:46}, end: {_id:51}, properties:{}}, {start: {_id:53}, end: {_id:58}, properties:{}}] AS row
MATCH (start:`UNIQUE IMPORT LABEL`{`UNIQUE IMPORT ID`: row.start._id})
MATCH (end:`UNIQUE IMPORT LABEL`{`UNIQUE IMPORT ID`: row.end._id})
CREATE (start)-[r:BELONGS_TO]->(end) SET r += row.properties;
UNWIND [{start: {_id:19}, end: {_id:26}, properties:{}}] AS row
MATCH (start:`UNIQUE IMPORT LABEL`{`UNIQUE IMPORT ID`: row.start._id})
MATCH (end:`UNIQUE IMPORT LABEL`{`UNIQUE IMPORT ID`: row.end._id})
CREATE (start)-[r:OF]->(end) SET r += row.properties;
UNWIND [{start: {_id:0}, end: {_id:1}, properties:{}}, {start: {_id:7}, end: {_id:8}, properties:{}}, {start: {_id:15}, end: {_id:1}, properties:{}}, {start: {_id:27}, end: {_id:28}, properties:{}}, {start: {_id:33}, end: {_id:34}, properties:{}}, {start: {_id:39}, end: {_id:28}, properties:{}}, {start: {_id:45}, end: {_id:46}, properties:{}}, {start: {_id:52}, end: {_id:53}, properties:{}}, {start: {_id:59}, end: {_id:28}, properties:{}}, {start: {_id:65}, end: {_id:53}, properties:{}}, {start: {_id:67}, end: {_id:53}, properties:{}}] AS row
MATCH (start:`UNIQUE IMPORT LABEL`{`UNIQUE IMPORT ID`: row.start._id})
MATCH (end:`UNIQUE IMPORT LABEL`{`UNIQUE IMPORT ID`: row.end._id})
CREATE (start)-[r:OF]->(end) SET r += row.properties;
UNWIND [{start: {_id:26}, end: {_id:25}, properties:{}}] AS row
MATCH (start:`UNIQUE IMPORT LABEL`{`UNIQUE IMPORT ID`: row.start._id})
MATCH (end:`UNIQUE IMPORT LABEL`{`UNIQUE IMPORT ID`: row.end._id})
CREATE (start)-[r:BELONGS_TO]->(end) SET r += row.properties;
UNWIND [{start: {_id:0}, end: {_id:2}, properties:{}}, {start: {_id:7}, end: {_id:9}, properties:{}}, {start: {_id:15}, end: {_id:3}, properties:{}}, {start: {_id:19}, end: {_id:21}, properties:{}}, {start: {_id:27}, end: {_id:29}, properties:{}}, {start: {_id:33}, end: {_id:35}, properties:{}}, {start: {_id:39}, end: {_id:40}, properties:{}}, {start: {_id:45}, end: {_id:47}, properties:{}}, {start: {_id:52}, end: {_id:54}, properties:{}}, {start: {_id:59}, end: {_id:60}, properties:{}}, {start: {_id:65}, end: {_id:54}, properties:{}}, {start: {_id:67}, end: {_id:55}, properties:{}}] AS row
MATCH (start:`UNIQUE IMPORT LABEL`{`UNIQUE IMPORT ID`: row.start._id})
MATCH (end:`UNIQUE IMPORT LABEL`{`UNIQUE IMPORT ID`: row.end._id})
CREATE (start)-[r:ORIGINATION]->(end) SET r += row.properties;
